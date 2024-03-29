// Written by William Boguslawski

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paragoomba : Goomba
{
    // reference to player position
    private GameObject player1;
    public float speed;
    public bool hunt = false;
    public float huntRadius = 34f; // radius to detect player

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player"); // find player
    }

    // Update is called once per frame
    void Update()
    {
        // if player isnt in sight dont hunt
        if (player1 == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player1.transform.position);
        if (distanceToPlayer <= huntRadius)
        {
            hunt = true;
        }
        else
        {
            hunt = false;
        }
        if (hunt == true)
        {
            Hunt();
        }
        else
            Turn();
            
    }

    private void Hunt()
    {
        //moving enemy position
        transform.position = Vector2.MoveTowards(transform.position, player1.transform.position, speed * Time.deltaTime); // position of enemy and then the position the enemy needs to move to 
    }

    // turns enemy to not look weird 
    private void Turn()
    {
        // if enemy in front of player dont turn toward player
        if (transform.position.x > player1.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // else enemy behind player, turn toward player
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }

    public void FindPlayer(GameObject newPlayer)
    {
        player1 = newPlayer;
    }

}

