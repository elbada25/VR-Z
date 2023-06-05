using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarColision : MonoBehaviour
{
    public GameObject objetoEstiradoPrefab; // Prefab del objeto que se estirará hacia delante
    public string tagObjetivo = "Zombie"; // Tag del objeto con el que se detectará la colisión

    private GameObject objetoEstirado; // Referencia al objeto instanciado
    private bool haChocado = false;

    private float velocidadEstiramiento = 505f;

    void Start()
    {
        // Instanciar el objeto que se estirará hacia delante
        //InstanciarObjeto();
    }

    void Update()
    {
        if (!haChocado)
        {
            Vector3 escala = objetoEstirado.transform.localScale;
            escala.z += velocidadEstiramiento * Time.deltaTime;
            objetoEstirado.transform.localScale = new Vector3(escala.x, escala.y, Mathf.Max(0f, escala.z));

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagObjetivo))
        {
            // Detectar la colisión con el objeto objetivo
            haChocado = true;
            Debug.Log("Chocado con un objeto de tag 'zombie'");

            // Detener el estiramiento del objeto
            objetoEstirado.GetComponent<Rigidbody>().isKinematic = true;

            // Llamar al método para eliminar el objeto
            
        }
        EliminarObjeto();
    }

    void EliminarObjeto()
    {
        // Destruir el objeto instanciado
        Destroy(objetoEstirado);

        // Reiniciar la variable haChocado
        haChocado = false;
    }

    public void InstanciarObjeto()
    {
        // Instanciar un nuevo objeto
        Debug.Log("Instaciado");

        objetoEstirado = Instantiate(objetoEstiradoPrefab, transform.position, transform.rotation);
    }
}
