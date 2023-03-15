using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;

    private bool shelled; // inficate if koopa is in shell or not
    private bool shellMoving; // indicate if koopa shell is moving or not


    // checks collision of koopa and player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player"))  // checks what koopa collides with
        {
            Player player = collision.gameObject.GetComponent<Player>(); // create reference to player script so you can call the player to get hit

            if (collision.transform.DotTest(transform, Vector2.down)) // checks if player lands on koopa head
            {
                EnterShell();
            }
            else
            {
                player.Hit();
            }
        }
    }

    // flatten method / death of goomba
    private void EnterShell()
    {
        shelled = true;
        GetComponent<EntityMovement>().enabled = false; // disables koopa movement
        GetComponent<AnimatedSprite>().enabled = false; // disables koopa animations
        GetComponent<SpriteRenderer>().sprite = shellSprite; // updates sprite to shell koopa
        
    }
}
