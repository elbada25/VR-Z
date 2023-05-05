using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform puntoSalida;
    public float velocidadBala = 10f;
    public float cadenciaDisparo = 0.1f;
    public float tiempoUltimoDisparo = 0f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time - tiempoUltimoDisparo > cadenciaDisparo)
        {
            tiempoUltimoDisparo = Time.time;

            GameObject nuevaBala = Instantiate(balaPrefab, puntoSalida.position, puntoSalida.rotation);
            Rigidbody rb = nuevaBala.GetComponent<Rigidbody>();
            rb.velocity = puntoSalida.forward * velocidadBala;
        }
    }
}
