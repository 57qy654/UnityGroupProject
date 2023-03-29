using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // checks what goomba collides with
        {
            Player player = collision.gameObject.GetComponent<Player>(); // create reference to player script so you can call the player to get hit
            player.Hit();
         
        }
    }
}
