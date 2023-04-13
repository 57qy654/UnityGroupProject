using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianOfHell : Goomba
{
    // reference to player position
    private GameObject player1;
    public float speed;
    public bool go = false;
    public float huntRadius = 34f; // radius to detect player
    private Vector2 initialPosition;
    private Vector2 currentPosition;
    private Vector2 targetPosition;
    private Vector2 midPosition;
    private bool movingTowardsTarget = true;
    public int count = 0;


    protected override void OnCollisionEnter2D(Collision2D collision)
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
                }
            }
            else if (collision.transform.DotTest(transform, Vector2.down)) // checks if player lands on goomba head
            {
                count++;
                if (count == 3)
                {
                    Flatten();
                    FindObjectOfType<AudioManager>().Play("Stomp");
                }
            }
            else
            {
                player.Hit();
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player"); // find player
        Physics2D.gravity = new Vector2(0, 0.0f); // set gravity to 0
        initialPosition = new Vector2(10, 8);
        targetPosition = new Vector2(-10, 8);
        midPosition = new Vector2(0, 8);
        EntityMovement movement = GetComponent<EntityMovement>();
        movement.speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // if player isnt in sight dont hunt
        if (player1 == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player1.transform.position);
        if (distanceToPlayer <= huntRadius)
        {
            go = true;
        }
        if (go == true)
        {
            EntityMovement movement = GetComponent<EntityMovement>();
            movement.speed = 3f;
            StartCoroutine(EnemyPattern());
        }




    }

    private IEnumerator StopUsing()
    {
        yield return new WaitForSeconds(1f);

    }

    private IEnumerator EnemyPattern()
    {
        yield return new WaitForSeconds(4f);

        currentPosition = transform.position;

        if (movingTowardsTarget)
        {
            // Move towards the target position

            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

            // Check if cerberus has reached the target position
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                // Set cerberus direction to move back to the initial position
                movingTowardsTarget = false;
            }
        }
        else
        {
            // Move towards the initial position
            transform.position = Vector2.MoveTowards(currentPosition, initialPosition, speed * Time.deltaTime);

            // Check if cerberus has reached the initial position
            if (Vector2.Distance(transform.position, initialPosition) < 0.1f)
            {
                // Set the cerberus direction to move towards the target position
                movingTowardsTarget = true;

            }
        }
        yield return new WaitForSeconds(5f);
        transform.position = Vector2.MoveTowards(currentPosition, player1.transform.position, speed * Time.deltaTime);
    }

    public void FindPlayer(GameObject newPlayer)
    {
        player1 = newPlayer;
    }


}