using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfSwamp : Koopa
{
    //public Sprite shellSprite;
    //public float shellSpeed = 15;

    private float delayTime = 1.5f;
    private float delayTime2 = 2.0f;
    //private bool shelled; // inficate if koopa is in shell or not
    //private bool shellPushed; // indicate if koopa shell is moving or not




    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.starpower) // checks if the player is in starpower, if so, hits koopa
            {
                Hit();
            }
            else
            {
                player.Hit();
            }
        }
    }


    private IEnumerator EnterShellAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        shelled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<AnimatedSprite>().enabled = false; // disables koopa animations
        GetComponent<SpriteRenderer>().sprite = shellSprite; // updates sprite to shell koopa
        FindObjectOfType<AudioManager>().Play("StompKoopa");
        EntityMovement movement = GetComponent<EntityMovement>();
        movement.speed = shellSpeed;
    }

    private IEnumerator Sneaky(float delayTime2)
    {
        yield return new WaitForSeconds(delayTime2);
        EntityMovement movement2 = GetComponent<EntityMovement>();
        movement2.direction = -movement2.direction;
        movement2.speed = shellSpeed + 5;

    }




    //EntityMovement movement = GetComponent<EntityMovement>();
    void Start()
    {
        StartCoroutine(EnterShellAfterDelay(delayTime));
        StartCoroutine(Sneaky(delayTime2));
    }

}
