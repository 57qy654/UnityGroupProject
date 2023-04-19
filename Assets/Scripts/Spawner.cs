using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnPoints; // store points where enemys spawn
    public GameObject[] enemies; // store all enemys 
    private GameObject player1;
    public bool spawnerFinish;
    public GameObject spawnerFinishGameObject;
    public float minTime; // The minimum time between spawns of enemys 
    public float maxTime; // The maximum time between spawns of enemys, choose how frequently enemies spawn
    public bool canSpawn; // tells spawner if enemy can spawn
    public float spawnTime; // how long enemies spawn for
    public int enemiesPresent; // checks how many enemies are in
    GameObject currentPoint; // point where enemy spawns 
    int index;

    public void SpawnEnemy()
    {
        index = Random.Range(0, spawnPoints.Length); // choose random spot in spawn points and store in index
        currentPoint = spawnPoints[index]; //set point where enemy spawn
        float timeInBetweenSpawn = Random.Range(minTime, maxTime);


        if (canSpawn == true) // if can spawn, then spawn enemy
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], currentPoint.transform.position, Quaternion.identity);
            enemiesPresent++;
        }

        Invoke("SpawnEnemy", timeInBetweenSpawn); // spawns enemys for the random time
        Invoke("Stop", 8f); // stops enemys from spawning after 8 seconds
        if (spawnerFinish == true)
        {
            // done spawning
            // makes spawner stop spawning
            spawnerFinishGameObject.SetActive(true);
        }
        
    }

    // method that stops enemys from spawning
    public void Stop()
    {
        CancelInvoke("SpawnEnemy"); // stops the Spawn enemy
    }
}
