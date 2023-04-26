using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;
    private Spawner goombaSpawn; // create reference to spawning script

    // checks collision of goomba and player
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // checks what goomba collides with
        {
            Player player = collision.gameObject.GetComponent<Player>(); // create reference to player script so you can call the player to get hit

            if (player.starpower) // checks if the player is in starpower, if so, hits goomba
            {
                Hit();
            }
            else if (collision.transform.DotTest(transform, Vector2.down)) // checks if player lands on goomba head
            {
                FindObjectOfType<AudioManager>().Play("stomp");
                Flatten();
            }
            else
            {
                player.Hit();
            }
        }

    }

    protected void OnTriggerEnter2D(Collider2D other) //checks to see what trigger goomba enters into
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell")) // checks to see if goomba enters the shell trigger zone
        {
            Hit();
        }

    }

    // flatten method / death of goomba
    protected void Flatten()
    {
        GetComponent<Collider2D>().enabled = false; // disables goomba collider
        GetComponent<EntityMovement>().enabled = false; // disables goomba movement
        GetComponent<AnimatedSprite>().enabled = false; // disables goomba animations
        GetComponent<SpriteRenderer>().sprite = flatSprite; // updates sprite to flat goomba
        Destroy(gameObject, 0.5f); // destroys goomba after half a second, so you are able to see flat goomba
        
        goombaSpawn = FindObjectOfType<Spawner>();
        if (goombaSpawn != null)
        {
            goombaSpawn.enemiesPresent--; // when enemy dies, tells spawner that enemy is dead
                                          //FindObjectOfType<AudioManager>().Play("stomp");

            //check if spawner done
            // without enemies present logic, the spawner would just stop when the spawn time reached 0
            if (goombaSpawn.spawnTime <= 0 && goombaSpawn.enemiesPresent <= 0)
            {
                goombaSpawn.spawnerFinish = true;
            }
        }
        //goombaSpawn.enemiesPresent--; // when enemy dies, tells spawner that enemy is dead
        //FindObjectOfType<AudioManager>().Play("stomp");

        //check if spawner done
        // without enemies present logic, the spawner would just stop when the spawn time reached 0
        /*
        if (goombaSpawn.spawnTime <= 0 && goombaSpawn.enemiesPresent <= 0)
        {
            goombaSpawn.spawnerFinish = true;
        }
        */
    }

    protected void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false; // reference to animated sprite script, stops animations
        GetComponent<DeathAnimation>().enabled = true; // reference to death animation script, kills goomba and does the death animation
        Destroy(gameObject, 3f); // destroy dead goomba after 3 seconds

        goombaSpawn = FindObjectOfType<Spawner>();
        goombaSpawn.enemiesPresent--; // when enemy dies, tells spawner that enemy is dead

        //check if spawner done
        // without enemies present logic, the spawner would just stop when the spawn time reached 0
        if (goombaSpawn.spawnTime <= 0 && goombaSpawn.enemiesPresent <= 0)
        {
            goombaSpawn.spawnerFinish = true;
        }

    }

     
}
