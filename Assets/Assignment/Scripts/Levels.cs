using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public GameObject[] enemySpawnPoints;
    public GameObject[] enemyPrefabs;
    public int enemiesRemaining = 0;
    public int totalEnemies;
    public int lvEnemies = 0;
    public int nextLV = 0;
    private ShipController ship;

    void Start()
    {
        totalEnemies = enemySpawnPoints.Length;
        nextLV = 1;
        ship = FindObjectOfType<ShipController>();
    }

    void Update()
    {
        if (enemiesRemaining == 0)
        {
            Debug.Log("Level Completed!");
            nextLV = 1;
        }
    }

    void SpawnEnemies(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            GameObject spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];

            Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);

            enemiesRemaining++;
        }
    }

    public void EnemyDestroyed()
    {
        enemiesRemaining--;
    }

    public void LoadNextLevel()
    {
        if (nextLV == 1){
        lvEnemies += 1;
        SpawnEnemies(lvEnemies);
        ship.EndRound();
            nextLV = 0;
    }
    }
}
