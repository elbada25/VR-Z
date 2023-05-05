using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float laserDistance = 100f;
    public LayerMask layerMask;

    private LineRenderer lineRenderer;
    private RaycastHit hit;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
    }

    void Update()
    {
        Vector3 startPos = transform.parent.position;
        Vector3 direction = transform.parent.forward;

        if (Physics.Raycast(startPos, direction, out hit, laserDistance, layerMask))
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, startPos + (direction * laserDistance));
        }
    }
}
