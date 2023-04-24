using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bowser : MonoBehaviour
{
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
    public GameObject player = GameObject.Find("Player");
    private Vector2 velocity;
    Transform playerTransform;


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

    
}
