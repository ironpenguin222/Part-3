using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Levels : MonoBehaviour
{
    // Enemy Variables
    public GameObject[] enemySpawnPoints;
    public GameObject[] enemyPrefabs;
    public int enemiesRemaining = 0;
    public int totalEnemies;
    public int lvEnemies = 0;

    // Other Variables
    public int nextLV = 0;
    private ShipController ship;
    public GameObject asteroidPrefab;



    void Start() // Starts game, starts off at next level, so that game can start when you click the next round button
    {
        totalEnemies = enemySpawnPoints.Length;
        nextLV = 1;
        ship = FindObjectOfType<ShipController>();


    }






    void Update()
    {
        if (enemiesRemaining == 0)
        {
            if (nextLV == 0)
            {
                Debug.Log("Level Completed!");
                nextLV = 1; // If enemies are all gone, and the next level variable hasnt been given to the player already, they are given the next level variable
            }

        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Toggle Asteroids on/off
            AsteroidToggle.AsteroidEnabled = !AsteroidToggle.AsteroidEnabled;
        }
    }

    void SpawnEnemies(int numEnemies) // Uses loop to spawn random enemies at random spawn points
    {
        for (int i = 0; i < numEnemies; i++)
        {
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            GameObject spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];

            Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);

            enemiesRemaining++;
        }
    }


    void SpawnAsteroids(int numAsteroids)
    {
        if (AsteroidToggle.AsteroidEnabled) // Checks if asteroids are enabled
        {
            // Defines the area where asteroids should spawn
            float minX = -10f;
            float maxX = 10f;
            float minY = -2f;
            float maxY = 2f;

            for (int i = 0; i < numAsteroids; i++)
            {
                // Generates random spawn positions within the defined bounds
                float randomX = Random.Range(minX, maxX);
                float randomY = Random.Range(minY, maxY);
                Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

                // Instantiates asteroid at the random spawn position
                Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
   


    public static class AsteroidToggle
    {
        public static bool AsteroidEnabled = true; // Sets asteroids to be enabled originally

    }


    public void EnemyDestroyed()
    {
        enemiesRemaining--; // Lets game know when an enemy is gone
    }

    public void LoadNextLevel() // loads the asteroids, spawns enemies, regains player health. Sets up the next round
    {
        if (nextLV == 1){
        lvEnemies += 1;
        SpawnEnemies(lvEnemies);
        SpawnAsteroids(3);
        ship.EndRound();
        nextLV = 0;
        }
    }
}
