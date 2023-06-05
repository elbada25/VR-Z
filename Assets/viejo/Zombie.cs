using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public NavMeshAgent enemy;
    Transform Player;
    public Animator animator;
    public int health = 100;
    public spawnZombies spawn;

    void Start()
    {
        // Buscar el objeto "Capsule" por su nombre y asignar su transform al campo Player
        GameObject playerObject = GameObject.Find("Capsule");
        if (playerObject != null)
        {
            Player = playerObject.transform;
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            return;
        }

        if (Vector3.Distance(transform.position, Player.position) < 1.5f)
        {
            enemy.isStopped = true;
            animator.SetTrigger("Atacar");
        }
        else
        {
            enemy.isStopped = false;
            animator.SetTrigger("Correr");
            enemy.SetDestination(Player.position);
            Vector3 direction = enemy.velocity.normalized;
            if (direction != Vector3.zero)
            {
                transform.LookAt(transform.position + direction);
            }
        }
    }

    public void TakeDamage(int damage)
{
    Debug.Log("Daño");
    health -= damage;
    if (health <= 0)
    {
        animator.SetTrigger("Morir");
        enemy.isStopped = true;
        spawn.deadZombie();

        // Esperar un corto tiempo antes de destruir el objeto del zombie
        StartCoroutine(DestruirZombie());
    }
}

private IEnumerator DestruirZombie()
{
    yield return new WaitForSeconds(1f);
    Destroy(gameObject);
}

}
