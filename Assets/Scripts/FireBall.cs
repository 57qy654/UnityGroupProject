using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 1f; // variable for speed
    public Vector2 direction = Vector2.left;    // variable for direction
    public float gravity = -9.81f; //allows you to customize the gravity per entity, useful for adding in the cheat menu or something

    private new Rigidbody2D rigidbody; // provides reference for rigid body in unity
    private Vector2 velocity; // variable for velocity

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    // specifys what happens when enable is active or inactive
    private void OnEnable()
    {
        rigidbody.WakeUp(); // allows rigid body to start moving
    }

    private void OnDisable()
    {
        rigidbody.velocity = Vector2.zero; // stop velocity when something like death happens
        rigidbody.Sleep(); // makes sure rigidbody doesn't move when disabled or not in vision
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x = direction.x * speed;
        

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime); // this line actually moves the rigid body

        if (rigidbody.Raycast(Vector2.left))
        {
            Destroy(this.gameObject);
            Debug.Log("Collider hit!");
            //FindObjectOfType<AudioManager>().Play("ShellRicochet");

        }

        if (rigidbody.Raycast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        } 
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        rigidbody.velocity = new Vector2(velocity.x, -velocity.y);
        //FindObjectOfType<AudioManager>().Play("IceHit");

        // checks if iceball collides with an enemy and kills it if it does
        if (col.collider.tag == "Enemy")
        {

            Destroy(col.gameObject);
            Destroy(this.gameObject);
            //FindObjectOfType<AudioManager>().Play("IceHit");
        }

        // deletes iceball when it comes in contact with a collider
        if(col.collider.tag != "Boss")
        {
            if (col.contacts[0].normal.x != 0)
            {
                Destroy(this.gameObject);
                //FindObjectOfType<AudioManager>().Play("IceHit");
            }
        }
        
    }
}
