using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnPoints; // store points where enemys spawn
    public GameObject[] enemies; // store all enemys 
    public bool spawnerFinish;
    public GameObject spawnerFinishGameObject;
    public float minTime; // The minimum time between spawns of enemys 
    public float maxTime; // The maximum time between spawns of enemys, choose how frequently enemies spawn
    public bool canSpawn; // tells spawner if enemy can spawn
    public float spawnTime; // how long enemies spawn for
    public int enemiesPresent; // checks how many enemies are in

    GameObject currentPoint; // point where enemy spawns 
    int index;

    private void Start()
    {
        Invoke("SpawnEnemy", 0.5f);
    }

    private void Update()
    {
        // will spawn every 3 seconds
        if (canSpawn)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime < 0)
            {
                spawnTime = 3;
            }
        }
    }

    void SpawnEnemy()
    {
        index = Random.Range(0, spawnPoints.Length); // choose random spot in spawn points and store in index
        currentPoint = spawnPoints[index]; //set point where enemy spawn
        float timeInBetweenSpawn = Random.Range(minTime, maxTime);

        if (canSpawn == true) // if can spawn, then spawn enemy
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], currentPoint.transform.position, Quaternion.identity);
            enemiesPresent++;
        }

        Invoke("SpawnEnemy", timeInBetweenSpawn);
        if (spawnerFinish == true)
        {
            // done spawning
            // makes spawner stop spawning
            spawnerFinishGameObject.SetActive(true);
        }
    }
}
