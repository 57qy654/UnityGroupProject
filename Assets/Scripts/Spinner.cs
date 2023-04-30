using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : Koopa
{

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // checks what koopa collides with, only relevant if not in shell
        {
            Player player = collision.gameObject.GetComponent<Player>(); // create reference to player script so you can call the player to get hit
            player.Hit();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // checks when player triggers the box collider 2D, only relevant when inside shell
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.Hit();

        }
    }
        // Start is called before the first frame update
    void Start()
    {
        shelled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<AnimatedSprite>().enabled = false; // disables koopa animations
        GetComponent<SpriteRenderer>().sprite = shellSprite; // updates sprite to shell koopa
        FindObjectOfType<AudioManager>().Play("StompKoopa");
        EntityMovement movement = GetComponent<EntityMovement>();
        movement.speed = shellSpeed;
        //nextStage = true;
    }



    
}
