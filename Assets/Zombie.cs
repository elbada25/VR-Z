using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform Player;
    public Animator animator;
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Si el enemigo está muerto, no hace nada
        if (health <= 0) {
            return;
        }

        // Si el enemigo está cerca del jugador, ataca
        if (Vector3.Distance(transform.position, Player.position) < 1.5f) {
            enemy.isStopped = true;
            animator.SetTrigger("Atacar");
        }
        else {
            // Si no está cerca, persigue al jugador
            enemy.isStopped = false;
            animator.SetTrigger("Correr");
            enemy.SetDestination(Player.position);
            // Rotate enemy to face direction of movement
            Vector3 direction = enemy.velocity.normalized;
            if (direction != Vector3.zero) {
                transform.LookAt(transform.position + direction);
            }
        }
    }

    public void TakeDamage(int damage) {
        // El enemigo pierde vida
        health -= damage;
        if (health <= 0) {
            // Si la salud llega a cero, el enemigo muere
            animator.SetTrigger("Morir");
            enemy.isStopped = true;
            Destroy(gameObject, 2f); // Destruye el objeto del enemigo después de dos segundos
        }
    }
}
