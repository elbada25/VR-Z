using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public List<Transform> spawnPositions;
    public float spawnInterval = 5f;
    public int maxZombies = 12;

    public int currentZombies = 0;
    private int totalZombies = 0;
    private int round = 1;

    private void Start()
    {
        StartCoroutine(SpawnZombiesCoroutine());
    }

    private IEnumerator SpawnZombiesCoroutine()
    {
        while (totalZombies < 5)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentZombies < maxZombies)
            {
                Transform spawnPosition = GetRandomSpawnPosition();
                Instantiate(zombiePrefab, spawnPosition.position, spawnPosition.rotation);
                currentZombies++;
                totalZombies++;
            }
        }

        while (currentZombies > 0)
        {
            yield return null;
        }

        round++;
        maxZombies = Mathf.RoundToInt(maxZombies * 1.25f);
        totalZombies = 0;

        StartCoroutine(SpawnZombiesCoroutine());
    }

    private Transform GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, spawnPositions.Count);
        return spawnPositions[randomIndex];
    }

    public void DecreaseZombieCount()
    {
        currentZombies--;
    }
}
