using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public GameObject spawning;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
          
            Spawner spawnerReference = GameObject.Find("Spawning").GetComponent<Spawner>(); //create reference to spawning object and spawn script
            
            spawnerReference.SpawnEnemy(); // call the spawn method that spawns enemys

            
        }
    }
}
