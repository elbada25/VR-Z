using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnZombies : MonoBehaviour
{
    public int activeZombies;
    public int maxZombies;
    public Transform spawn;
    public GameObject zombie;
    public float spawnInterval;

    private float timer=0f;

    void Start()
    {
        activeZombies = 0;
        maxZombies = 12;
        spawnInterval = 15f;
    }

   void Update()
{
    timer += Time.deltaTime;

    if (timer >= spawnInterval && activeZombies < maxZombies)
    {
        GameObject newZombie = Instantiate(zombie, spawn.position, spawn.rotation);
        newZombie.tag = "Zombie";
        activeZombies++;
        timer = 0f;
    }
}


    public void deadZombie(){
        activeZombies--;
    }
}
