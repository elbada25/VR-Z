using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float tiempoVida = 3f;
    public int danioBala = 10; // daño que hace cada bala

    // Se llama cuando un collider entra en contacto con este collider (que está marcado como isTrigger)
    private void OnTriggerEnter(Collider other)
    {
        Zombie zombie = other.GetComponent<Zombie>();
        if(zombie != null) // si el objeto colisionado tiene el componente Zombie3
        {
            zombie.TakeDamage(danioBala); // llamar al método RecibirDanio de la clase Zombie3 para hacer daño al enemigo
            Destroy(gameObject); // destruir la bala
        }
    }

    // Destruir la bala después de un tiempo establecido
    private void Start()
    {
        Destroy(gameObject, tiempoVida);
    }
}