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
    public bool canSpawn = true; // tells spawner if enemy can spawn
    public float spawnTime; // how long enemies spawn for
    public int enemiesPresent; // checks how many enemies are in
    //public bool permission;
    public float spawnRadius = 10f;
    //bool isInvoking = true;


    GameObject currentPoint; // point where enemy spawns 
    int index;

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player"); // find player
        permission = true;


    }


    private void Update()
    {
        if (player1 == null)
            return;
        float distanceToPlayer = Vector2.Distance(transform.position, player1.transform.position);

        if (distanceToPlayer <= spawnRadius && )
        {
            if (permission == true)
            {
                
                Invoke("SpawnEnemy", 0.5f);
                permission = false;
                isInvoking = true;
                distanceToPlayer = 64;

                if (canSpawn)
                {
                    spawnTime -= Time.deltaTime; // acts as a count down
                    if (spawnTime < 0)
                    {
                        canSpawn = false;
                    }
                }
            }
            
        }
        else
        {
            // Cancel the Invoke call if player is not in the spawn radius
            if (isInvoking)
            {
                CancelInvoke("SpawnEnemy");
                isInvoking = false;
            }
        }

    }

    void SpawnEnemy()
    {
        index = Random.Range(0, spawnPoints.Length); // choose random spot in spawn points and store in index
        currentPoint = spawnPoints[index]; //set point where enemy spawn
        //float timeInBetweenSpawn = Random.Range(minTime, maxTime);
        float timeInBetweenSpawn = minTime;

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
