// Written by William Boguslawski, Jude Pitschka, Jessica Nguyen

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;

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
    private Vector2 velocity;
    Transform playerTransform;
    public int count = 0;
    public Sprite BowserDead;
    public bool canFire = true;
    public GameObject iceFlower;
    public GameObject iceFlower2;
    public AudioSource audioSource;


    public Player marioScript;

    private ShootSomething shootSomething; // reference to script that makes boss shoot fireballs


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

        shootSomething = GetComponent<ShootSomething>();
        shootSomething.CanShoot = true;
        marioScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

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

        if (Time.time - lastFireballTimes > fireballDelay && canFire)
        {
            BossShoot();
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
            player.Hit();
        }

        if (collision.gameObject.CompareTag("Ice"))
        {
            count++;
            FindObjectOfType<AudioManager>().Play("BowserHit");
            if (count == 1)
            {
                Spawner spawnerReference = GameObject.Find("Spawning").GetComponent<Spawner>(); //create reference to spawning object and spawn script

                spawnerReference.SpawnEnemy(); // call the spawn method that spawns enemys
                iceFlower.SetActive(true);
                shootSomething.cooldown = 2f;
                marioScript.Hit();                           
            }
            if (count == 2)
            {
                Spawner spawnerReference = GameObject.Find("Spawning").GetComponent<Spawner>(); //create reference to spawning object and spawn script

                spawnerReference.SpawnEnemy(); // call the spawn method that spawns enemys
                iceFlower2.SetActive(true);
                shootSomething.cooldown = 1f;
                marioScript.Hit();

            }
            if (count == 3)
            {
                Hit();
                audioSource.enabled = false;
                AudioManager audioManager = FindObjectOfType<AudioManager>();
                audioManager.Play("YouWin");
            }
        }
            
    }

    protected void Hit()
    {
        canFire = false;
        FindObjectOfType<AudioManager>().Play("BowserDie");
        GetComponent<Collider2D>().enabled = false; // disables bowser collider
        GetComponent<AnimatedSprite>().enabled = false; // disables  animations
        GetComponent<SpriteRenderer>().sprite = BowserDead; // updates sprite hit bowser

        Destroy(gameObject, 0.5f); // destroy dead goomba after 3 seconds
        FindObjectOfType<AudioManager>().Stop("BowserDie");

    }

    public void BossShoot()
    {
        shootSomething.Fire2();
    }

   
}
