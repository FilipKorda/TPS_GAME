using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject enemyTwoPrefab;
    public float spawnRandomSpawnPoint = 1.0f;
    public float spawnAllSpawnPoints = 5.0f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnRandomSpawnPoint, spawnRandomSpawnPoint);
        InvokeRepeating("SpawnEnemyWithDelay", spawnAllSpawnPoints, spawnAllSpawnPoints);
    }

    void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[spawnIndex].transform.position;
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        newEnemy.GetComponent<EnemyAI>().player = GameObject.FindWithTag("Player").transform;
    }

    void SpawnEnemyWithDelay()
    {
        foreach (GameObject spawnPoint in spawnPoints)
        {
            Vector3 spawnPosition = spawnPoint.transform.position;
            GameObject newEnemy = Instantiate(enemyTwoPrefab, spawnPosition, Quaternion.identity);
            newEnemy.GetComponent<EnemyAI>().player = GameObject.FindWithTag("Player").transform;
        }
    }
}
