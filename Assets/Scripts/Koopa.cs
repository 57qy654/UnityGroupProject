using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12;

    protected bool shelled; // inficate if koopa is in shell or not
    protected bool shellPushed; // indicate if koopa shell is moving or not


    // checks collision of koopa and player
    // private virtual void OnCollisionEnter2D(Collision2D collision)
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player"))  // checks what koopa collides with, only relevant if not in shell
        {
            Player player = collision.gameObject.GetComponent<Player>(); // create reference to player script so you can call the player to get hit

            if (player.starpower) // checks if the player is in starpower, if so, hits koopa
            {
                Hit();
            }
            else if (collision.transform.DotTest(transform, Vector2.down)) // checks if player lands on koopa head
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

                if (player.starpower) // checks if the player is in starpower, if so, hits koopa shell
                {
                    Hit(); 
                }
                else
                {
                    player.Hit();
                }

                player.Hit();

            }
        }
        else if (!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell")) // if koopa is not shelled and collides with another shell
        {
            Hit(); // koopa dies
        }
    }

    // koopa hides in shell when mario jumps on him
    private void EnterShell()
    {
        shelled = true;
        GetComponent<EntityMovement>().enabled = false; // disables koopa movement
        GetComponent<AnimatedSprite>().enabled = false; // disables koopa animations
        GetComponent<SpriteRenderer>().sprite = shellSprite; // updates sprite to shell koopa
        FindObjectOfType<AudioManager>().Play("StompKoopa");

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

 

    protected void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false; // reference to animated sprite script, stops animations
        GetComponent<DeathAnimation>().enabled = true; // reference to death animation script, kills koopa and does the death animation
        Destroy(gameObject, 3f); // destroy dead koopa after 3 seconds

    }

    /*
     
    extra option to add if you want that destroys koopa shell if it goes out of vision
    private void OnBecameInvisible()
    {
        if (shellPushed)
        {
            Destroy(gameObject);
        }

    }

    */
}
