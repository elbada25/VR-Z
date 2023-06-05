using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OculusSampleFramework;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class arma : MonoBehaviour
{
    /*
    public int capacidadMaxima = 10;       // Capacidad máxima de municiones del arma
    public float cadenciaDisparo = 0.33f;  // Cadencia de disparo en segundos (3 balas por segundo)
    public int dañoZombie = 10;            // Daño infligido a los zombies
    public float tiempoRecarga = 2f;       // Tiempo de recarga en segundos

    private int municiones;                // Municiones actuales del arma
    private bool puedeDisparar = true;      // Indica si el arma puede disparar en el momento actual
    

    public XRController controller;
    public Transform municionText;
    public Transform salida;

    public int damagePerShot = 10;
    public float timeBetweenShots = 0.33f;
    public int magazineSize = 50;
    public float reloadTime = 2f;

    private int currentAmmo =50;
    private bool isReloading;
    private float nextShotTime;

    private XRRayInteractor rayInteractor; 
    public DetectarColision DetectarColision;

    private void Start()
    {
        //municiones = capacidadMaxima;       // Inicializar las municiones al máximo al inicio
        //rayInteractor = GetComponent<XRRayInteractor>();
    }

    private void Update()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.5f && Time.time >= nextShotTime)
        {
            nextShotTime = Time.time + 1f / 3;
            disparar();
            currentAmmo--;
            //Shoot();
        }
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        ActualizarTextoMunicion();
    }
    
    private void Disparar()
    {
        // Crear un rayo desde la posición y dirección de la cámara principal
        Ray rayo = new Ray(salida.position, salida.forward);
        RaycastHit hit;

        if (Physics.Raycast(rayo, out hit))
        {
            // Verificar si el rayo impactó en un objeto con el tag "Zombie"
            if (hit.collider.CompareTag("Zombie"))
            {
                Debug.Log("Impacta en zombie");
                // Reducir la vida del zombie afectado por el daño del arma
                Zombie zombie = hit.collider.GetComponent<Zombie>();
                zombie.TakeDamage(dañoZombie);
            }
        }

        municiones--;   // Reducir una munición después de disparar

        if (municiones <= 0)
        {
            // Si se acabaron las municiones, iniciar la recarga
            puedeDisparar = false;
            Invoke("Recargar", tiempoRecarga);
        }
        else
        {
            // Esperar la cadencia de disparo antes de poder disparar nuevamente
            puedeDisparar = false;
            Invoke("HabilitarDisparo", cadenciaDisparo);
        }
    }

    private void Recargar()
    {
        municiones = capacidadMaxima;   // Recargar las municiones al máximo
        puedeDisparar = true;           // Habilitar el disparo después de recargar
    }

    private void HabilitarDisparo()
    {
        puedeDisparar = true;   // Habilitar el disparo después de la cadencia de disparo
    }

    private void ActualizarTextoMunicion()
    {
        if (municionText != null)
        {
            municionText.GetComponent<Text>().text = currentAmmo.ToString();
        }
    }*/
}
