using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianOfHell : Goomba
{
    private GameManager gameManager;  // add a reference to the game manager

    private GameObject player1;  // reference to player game object
    public Transform player2; // a reference to the players transform, drag mario into slot in the editor
    public float speed; // referene to speed
    public bool go = false; // variable that determines if boss moves or not
    public float huntRadius = 28f; // radius to detect player
    private Vector2 initialPosition;   // variable for start position
    private Vector2 currentPosition;    // variable for position of enemy
    private Vector2 markPosition;   // variable for position want to move to
    public int count = 0;   // variable that determines boss healthbar
    private bool isMovingLeft = true;
    private bool hunt;  // variable for attack

    private ShootSomething shootSomething; // reference to script that makes boss shoot fireballs

    // Start is called before the first frame update

    
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player"); // find player
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        initialPosition = transform.position;   // initialize start position
        markPosition = new Vector2(transform.position.x - 20, transform.position.y);
        hunt = false; // set attack to be false

        // get the reference to game manager script to have it change levels when boss dies.
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        shootSomething = GetComponent<ShootSomething>();
        shootSomething.CanShoot = true;

        //BossShoot();

        //Physics2D.gravity = new Vector2(0, 0.0f); // set gravity to 0
        //initialPosition = new Vector2(10, 8); // initialize start position
        //markPosition = new Vector2(-10, 8);   // initialize target position
    }

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
                    gameManager.NextLevel();
                }
            }
            else if (collision.transform.DotTest(transform, Vector2.down)) // checks if player lands on goomba head
            {
                count++;
                if (count == 3)
                {
                    Flatten();
                    FindObjectOfType<AudioManager>().Play("Stomp");
                    gameManager.NextLevel();
                }
            }
            else
            {
                player.Hit();
            }
        }

    }



    public void BossShoot()
    {
        if (go == true)
        {
            shootSomething.Fire();
        }
        //shootSomething.Fire();
    }
    // Update is called once per frame
    void Update()
    {

        // if player isnt in sight dont hunt
        if (player1 == null)
            return;

        BossShoot();
        float distanceToPlayer = Vector2.Distance(transform.position, player1.transform.position);
        if (distanceToPlayer <= huntRadius)
        {
            go = true;  // if within radius boss is allowed to move
        }
        if (go == true)
        {
            if (isMovingLeft) // used so that Moveleft doesnt get called more than once so that it doesnt jitter when changing directions
            {
                StartCoroutine(enemyPattern()); // start enemy pattern method
                //BossShoot();
                

            }
            if (isMovingLeft != true && hunt == true)
            {
                go = false;
                Hunt(); //when done with enemy pattern attack player
            }

        }
    }

    public void FindPlayer(GameObject newPlayer)
    {
        player1 = newPlayer;
    }

    
    public IEnumerator enemyPattern()
    {
        GameObject theBoss = GameObject.Find("Cerberus"); // create reference to game object of the boss
        float distanceBetweenStartAndFlip = initialPosition.x - markPosition.x; // the distance between the 2 points
        EntityMovement bossMovement = theBoss.GetComponent<EntityMovement>(); // reference to entitymovement script of boss object
        //ShootSomething bossAttack = theBoss.GetComponent<ShootSomething>();

        

        // Calculate the time it takes to reach the point where it will turn right
        float timeTillFlip = (distanceBetweenStartAndFlip / speed);

        // the boss should be placed 2 spaces to the left of where the initial position is in order to stop jitter
        yield return new WaitForSeconds(timeTillFlip);
        isMovingLeft = false;   
        bossMovement.direction = Vector2.right; // switches directions to move right

        yield return new WaitForSeconds(timeTillFlip);
        bossMovement.direction = Vector2.left; // switches directions to move left
        hunt = true;    // activate hunt
    }
    

    
    private void Hunt()
    {
        Vector2 direction = player2.position - transform.position;  // finding the direction to which the boss moves towards the player

        // makes it so that the object moving towards the player is at a constant speed, regardless of the distance between the boss & player.
        direction.Normalize();

        // moving the boss towards the player in the X and Y directions since its a flying type enemy
        transform.position += new Vector3(direction.x, direction.y, 0f) * speed * Time.deltaTime;
    }

    
    
}


