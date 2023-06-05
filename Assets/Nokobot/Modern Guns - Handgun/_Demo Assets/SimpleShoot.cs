using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using OculusSampleFramework;

public class SimpleShoot : MonoBehaviour
{
    
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [SerializeField] private float destroyTimer = 2f;
    [SerializeField] private float destroyTimerBullet = 5f;
    [SerializeField] private float shotPower = 500f;
    [SerializeField] private float ejectPower = 650f;

    public float municion=12;
    public float maxMunicion=12;
    public AudioSource audio;
    public AudioClip disp, noDips, recargar;
    public Transform objetoConText;
    private Text textoComponent;

    public Image fillImage;
    public float fillDuration = 1f;

    public bool Derecha = true;

    private bool isReloading = false;


    public GameObject zombiePrefab;
    public List<Transform> spawnPositions;
    public float spawnInterval = 1f;
    public int maxZombies = 12;

    public int currentZombies = 0;
    private int totalZombies = 0;
    public int round = 1;
    public int totalKills = 0;
    
    private bool siguienteRonda;

    void Start()
    {
        if (barrelLocation == null)
        {
            barrelLocation = transform;
        }

        if (gunAnimator == null)
        {
            gunAnimator = GetComponentInChildren<Animator>();
        }
        textoComponent = objetoConText.GetComponent<Text>();

        StartCoroutine(SpawnZombiesCoroutine());
    }

    IEnumerator ResetAmmo()
    {
        if (isReloading) yield break; // Si ya se está recargando, salir de la corutina
        isReloading = true; // Marcar como recargando
        audio.PlayOneShot(recargar);
        StartCoroutine(FillCoroutine());
        yield return new WaitForSeconds(1f); // Esperar dos segundos
        municion = maxMunicion;
        textoComponent.text = municion.ToString();
        isReloading = false; // Marcar como finalizada la recarga
    }

    private IEnumerator FillCoroutine()
    {
        float timer = 0f;

        while (timer < fillDuration)
        {
            timer += Time.deltaTime;
            fillImage.fillAmount = timer / fillDuration;
            yield return null;
        }

        fillImage.fillAmount = 0f;
    }
    
    void Update()
    {
        if(Derecha)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                if (municion > 0 && !isReloading) // Agregar verificación de !isReloading para evitar disparar durante la recarga
                {
                    gunAnimator.SetTrigger("Fire");
                    municion--;
                    audio.PlayOneShot(disp);
                }
                else if (municion <= 0)
                {
                    audio.PlayOneShot(noDips);
                }
                textoComponent.text = municion.ToString();
                //Calls animation on the gun that has the relevant animation events that will fire
            }

            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
            {
                StartCoroutine(ResetAmmo());
            }
        }else{
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
            {
                if (municion > 0 && !isReloading) // Agregar verificación de !isReloading para evitar disparar durante la recarga
                {
                    gunAnimator.SetTrigger("Fire");
                    municion--;
                    audio.PlayOneShot(disp);
                }
                else if (municion <= 0)
                {
                    audio.PlayOneShot(noDips);
                }
                textoComponent.text = municion.ToString();
                //Calls animation on the gun that has the relevant animation events that will fire
            }

            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
            {
                StartCoroutine(ResetAmmo());
            }
        }
    }
    


    //This function creates the bullet behavior
    void Shoot()
    {
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        RaycastHit hitLocationInfo;
        bool hit = Physics.Raycast(barrelLocation.position, barrelLocation.forward, out hitLocationInfo, 150);
        if (hit && hitLocationInfo.rigidbody != null)
        {
            var tagOfHitPoint = hitLocationInfo.rigidbody.tag;
            if (tagOfHitPoint == "Zombie")
            {
                hitLocationInfo.collider.SendMessageUpwards("KillZombie", hitLocationInfo, SendMessageOptions.DontRequireReceiver);
            }
        }

        // Cancels if there's no bullet prefab
        if (!bulletPrefab)
        {
            return;
        }

        // Create a bullet and add force on it in direction of the barrel
        GameObject bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

        // Destroy the bullet after 3 seconds
        Destroy(bullet, 3f);
    }


    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
        
    }



    private IEnumerator SpawnZombiesCoroutine()
    {
        if(Derecha)
        {
            while (true)
            {
                while (currentZombies > 0)
                {
                    yield return null;
                }

                for (int i = 0; i < totalZombies; i++)
                {
                    yield return new WaitForSeconds(spawnInterval);

                    if (currentZombies < maxZombies)
                    {
                        Transform spawnPosition = GetRandomSpawnPosition();
                        GameObject zombieInstance = Instantiate(zombiePrefab, spawnPosition.position, spawnPosition.rotation);
                        zombieInstance.GetComponent<zombieRagdoll>().OnZombieDestroyed += DecreaseZombieCount;
                        currentZombies++;
                    }
                }

                Debug.Log("Ronda " + round);
                round++;
                totalZombies = Mathf.RoundToInt(totalZombies * 1.25f);
            }
        }
    }


    private Transform GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, spawnPositions.Count);
        return spawnPositions[randomIndex];
    }

    public void DecreaseZombieCount()
    {
        currentZombies--;
        totalKills++;
    }


}
