using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OculusSampleFramework;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class disparar : MonoBehaviour
{
    public Transform firePoint;
    public int damage = 10;
    public LayerMask enemyLayer;

    public void Shoot()
    {
        RaycastHit[] hits = Physics.RaycastAll(firePoint.position, firePoint.forward, Mathf.Infinity, enemyLayer);
        foreach (RaycastHit hit in hits)
        {
            Zombie zombie = hit.collider.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }
    }
}
