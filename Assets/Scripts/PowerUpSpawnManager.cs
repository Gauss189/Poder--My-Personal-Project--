using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject powerUp;

    private float startDelay = 1.5f;
    private float powerUpSpawnTime = 8f;
    private float spawnRange = 10f;

    void Start()
    {
        InvokeRepeating("SpawnPowerUp", startDelay, powerUpSpawnTime);
    }

    private void SpawnPowerUp()
    {
        float randomSpawnRange = Random.Range(spawnRange, -spawnRange);
        Vector3 spawnPos = new Vector3(randomSpawnRange, 0, randomSpawnRange);

        Instantiate(powerUp, spawnPos, powerUp.gameObject.transform.rotation);
    }
}
