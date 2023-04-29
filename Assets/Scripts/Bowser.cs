using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bowser : MonoBehaviour
{
    private GameManager gameManager;

    private Rigidbody2D rb2d;
    // Bowser only shows up in the castle scene
    public GameObject FireBall;
    // You can change the speed of the fireball to fit the game 
    public float fireballSpeed = 10.0f;
    //You can change the delay of the fireball to fit the game
    public float fireballDelay = 1.0f;
    public float fireballOffset = -3f;
    // You can change the max health if needed 
    public int maxHealth = 3;
    // new transform object to get the player's coordinates
    //public GameObject player = GameObject.Find("Player");
    private Vector2 velocity;
    Transform playerTransform;
    public int count = 0;
    public Sprite BowserDead;
    


    private int currentHealth;
    private float lastFireballTimes = 0.0f;

    private void Start()
    {
        currentHealth = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0f;
        // Find the player object by name
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        float bowserX = transform.position.x;
        float bowserY = transform.position.y;

        // reference to gameManager
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    public float delayTime = 10.0f;
    private Vector2 targetPosition;
    private float lastUpdateTime;

    private void Update()
    {
        float playerXPosition = playerTransform.position.x;
        float playerYPosition = playerTransform.position.y;
        if (Time.time - lastUpdateTime > delayTime)
        {
            
            targetPosition = new Vector2(playerXPosition, playerYPosition);
            lastUpdateTime = Time.time;
        }

        Vector2 bowserPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = Vector2.Lerp(bowserPosition, targetPosition, Time.deltaTime * 0.5f);
        transform.position = new Vector3(newPosition.x, transform.position.y, transform.position.z);

        if (playerXPosition > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Time.time - lastFireballTimes > fireballDelay)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, fireballOffset, 0);
            GameObject fireballClone = Instantiate(FireBall, spawnPosition, transform.rotation);
            FindObjectOfType<AudioManager>().Play("FireBall");
            Rigidbody2D rb = fireballClone.GetComponent<Rigidbody2D>();
            rb.velocity = (playerTransform.position - transform.position).normalized * fireballSpeed;
            lastFireballTimes = Time.time;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // checks what goomba collides with
        {
            Player player = collision.gameObject.GetComponent<Player>(); // create reference to player script so you can call the player to get hit

            if (player.starpower) // checks if the player is in starpower, if so, hits goomba
            {
                count++;
                if (count == 3)
                {
                    Hit();
                    gameManager.NextLevel();
                }
            }
            else if (collision.transform.DotTest(transform, Vector2.down)) // checks if player lands on goomba head
            {
                count++;
                if (count == 3)
                {
                    Hit();
                    FindObjectOfType<AudioManager>().Play("BowserHit");
                    gameManager.NextLevel();
                }
            }
            else
            {
                player.Hit();
            }
        }

    }

    protected void Hit()
    {
        FindObjectOfType<AudioManager>().Play("BowserDie");
        GetComponent<Collider2D>().enabled = false; // disables bowser collider
        //GetComponent<EntityMovement>().enabled = false; // disables goomba movement
        GetComponent<AnimatedSprite>().enabled = false; // disables goomba animations
        GetComponent<SpriteRenderer>().sprite = BowserDead; // updates sprite hit bowser

        //GetComponent<AnimatedSprite>().enabled = false; // reference to animated sprite script, stops animations
        //GetComponent<DeathAnimation>().enabled = true; // reference to death animation script, kills goomba and does the death animation
        Destroy(gameObject, 3f); // destroy dead goomba after 3 seconds

    }
    protected void Flatten()
    {
        
        GetComponent<Collider2D>().enabled = false; // disables bowser collider
        GetComponent<EntityMovement>().enabled = false; // disables goomba movement
        GetComponent<AnimatedSprite>().enabled = false; // disables goomba animations
        GetComponent<SpriteRenderer>().sprite = BowserDead; // updates sprite hit bowser
        Destroy(gameObject, 0.5f); // bowser dies after 3 seconds
    }
}
