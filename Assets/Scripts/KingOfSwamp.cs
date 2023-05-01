// Written by William Boguslawski

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfSwamp : Koopa
{
    private bool startPattern = true;
    private bool nextStage = false;
    private float delayTime = 1.9f;
    private float delayTime2 = 2.1f;
    private EntityMovement visible;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();


            if (player.starpower) // checks if the player is in starpower, if so, hits koopa
            {
                Hit();
                PlayerMovement mario = GameObject.Find("Mario").GetComponent<PlayerMovement>();
                mario.velocity = Vector2.zero;
                mario.moveSpeed = 0f;
                FallingBlock fallingBlock = GameObject.Find("FallingBlock (1)").GetComponent<FallingBlock>();
                StartCoroutine(fallingBlock.Tumble());

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

            Player player = other.GetComponent<Player>(); // create reference for player / mario

            if (player.starpower) // checks if the player is in starpower, if so, hits koopa shell
            {
                Hit();
                PlayerMovement mario = GameObject.Find("Mario").GetComponent<PlayerMovement>();
                mario.velocity = Vector2.zero;
                mario.moveSpeed = 0f;
                FallingBlock fallingBlock = GameObject.Find("FallingBlock (1)").GetComponent<FallingBlock>();
                StartCoroutine(fallingBlock.Tumble());
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
        nextStage = true;
    }

    private IEnumerator Sneaky(float delayTime2)
    {
        yield return new WaitForSeconds(delayTime2);
        EntityMovement movement2 = GetComponent<EntityMovement>();
        movement2.direction = -movement2.direction;
        movement2.speed = shellSpeed + 5;

    }



    void FixedUpdate()
    {
        if (visible.enabled == true && startPattern == true)
        {
            StartCoroutine(EnterShellAfterDelay(delayTime));
            startPattern = false;
            nextStage = true;
            if (nextStage == true)
            {
                StartCoroutine(Sneaky(delayTime2));
            }
        }

    }

    void Start()
    {
        visible = GetComponent<EntityMovement>();
    }

}
