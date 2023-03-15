using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12;

    private bool shelled; // inficate if koopa is in shell or not
    private bool shellPushed; // indicate if koopa shell is moving or not


    // checks collision of koopa and player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player"))  // checks what koopa collides with, only relevant if not in shell
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

    private void OnTriggerEnter2D(Collider2D other) // checks when player triggers the box collider 2D, only relevant when inside shell
    {
        if (shelled && other.CompareTag("Player"))
        {
            if (!shellPushed)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f); // determine which angle mario pushes shell
                PushShell(direction);
            }
            else  // if shell is moving from a push and you touch it again then mario gets hit and dies, same with all other enemys
            {
                Player player = other.GetComponent<Player>(); // create reference for player / mario
                player.Hit();

            }
        }

    }

    // koopa hides in shell when mario jumps on him
    private void EnterShell()
    {
        shelled = true;
        GetComponent<EntityMovement>().enabled = false; // disables koopa movement
        GetComponent<AnimatedSprite>().enabled = false; // disables koopa animations
        GetComponent<SpriteRenderer>().sprite = shellSprite; // updates sprite to shell koopa
        
    }

    private void PushShell(Vector2 direction)
    {
        shellPushed = true;

        // renable rigid body so it can move again
        // when isKinematic = false, the physics engine handles its physics movement 
        GetComponent<Rigidbody2D>().isKinematic = false; // get reference for rigidbody

        // renable custom entity movement script
        // temporary reference for entity movement
        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized; // direction of shell based on push direction
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell"); // updating shell layer, will allow collisions between enemies & shell to occur

    }


}
