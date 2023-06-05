using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class zombieRagdoll : MonoBehaviour
{
    public int vida = 30; // Vida inicial
    private bool ragdollActivated = false;
    public UnityEngine.AI.NavMeshAgent enemy;

    private Animator animator;
    Transform Player;

    public event Action OnZombieDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject playerObject = GameObject.Find("OVRCameraRig");
        if (playerObject != null)
        {
            Player = playerObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!ragdollActivated && vida <= 0)
        {
            RagDoll(true);
            ragdollActivated = true;
        }

        if(vida>=0)
        {
            enemy.SetDestination(Player.position);
            

            // Check if the gameobject is close to the player
            float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
            if (distanceToPlayer <= 2)
            {
                // If the gameobject is close to the player, play the attack animation
                animator.SetTrigger("atacar");
            }
            else
            {
                Vector3 direction = enemy.velocity.normalized;
                if (direction != Vector3.zero)
                {
                    transform.LookAt(transform.position + direction);
                }
                // If the gameobject is not close to the player, play the default animation
                animator.SetTrigger("correr");
            }
        }

    }

    void RagDoll(bool value)
    {
        var bodyParts = GetComponentsInChildren<Rigidbody>();
        foreach (var bodyPart in bodyParts)
        {
            bodyPart.isKinematic = !value;
        }
    }

    void KillZombie(RaycastHit hitLocationInfo)
    {
        if (vida <= 0) return; // Si la vida ya es menor o igual a cero, no hacer nada

        vida -= 10; // Reducir la vida en 10

        if (vida <= 0)
        {
            GetComponent<Animator>().enabled = false;
            RagDoll(true);
            Vector3 hitPoint = hitLocationInfo.point;

            var colliders = Physics.OverlapSphere(hitPoint, 0.5f);
            foreach (var collider in colliders)
            {
                var rigidbody = collider.GetComponent<Rigidbody>();
                if (rigidbody)
                {
                    rigidbody.AddExplosionForce(1000, hitPoint, 0.5f);
                }
            }

            Destroy(gameObject, 1f);

            // Llamar al evento OnZombieDestroyed si hay suscriptores
            OnZombieDestroyed?.Invoke();
        }
    }
}
