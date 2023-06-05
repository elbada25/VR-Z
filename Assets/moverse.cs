using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class moverse : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 movementDirection;
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        // Obtener la entrada del joystick del mando izquierdo
        float horizontalAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;
        float verticalAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;

        // Calcular la dirección de movimiento en relación a la cámara
        Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(cameraTransform.right, new Vector3(1, 0, 1)).normalized;
        movementDirection = cameraForward * verticalAxis + cameraRight * horizontalAxis;

        // Mover el GameObject
        transform.Translate(movementDirection * speed * Time.deltaTime);
    }

    
}
