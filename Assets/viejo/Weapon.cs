using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OculusSampleFramework;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Weapon : MonoBehaviour
{
    
    public float fireRate = 2f; // 2 rayos por segundo
    public int maxAmmo = 50;
    public float reloadTime = 2f;
    
    public int damage = 10;

    private int currentAmmo;
    private bool isReloading;
    private float nextFireTime;
    public disparar disp;
    private XRController controller;
    public Transform municionText;

    private void Start()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
        controller = GetComponent<XRController>();
    }

    private void Update()
{
    ActualizarTextoMunicion();
    if (isReloading)
    {
        return; // Espera a que termine la recarga
    }

    if (controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButtonValue) && triggerButtonValue && Time.time >= nextFireTime)
    {
        if (currentAmmo > 0)
        {
            disp.Shoot();
            currentAmmo--;

            if (currentAmmo <= 0)
            {
                // Inicia la recarga
                isReloading = true;
                Invoke("Reload", reloadTime);
            }
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
}


    private void ActualizarTextoMunicion()
    {
        if (municionText != null)
        {
            municionText.GetComponent<Text>().text = currentAmmo.ToString();
        }
    }

    private void Reload()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
