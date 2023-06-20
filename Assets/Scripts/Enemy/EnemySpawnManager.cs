using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyArray;

    private float startDelay = 1.5f;
    private float enemySpawnTime = 12f;

    void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", startDelay, enemySpawnTime);
    }

    private void SpawnRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemyArray.Length);
        Instantiate(enemyArray[randomIndex], transform.position, enemyArray[randomIndex].gameObject.transform.rotation);
    }
}
