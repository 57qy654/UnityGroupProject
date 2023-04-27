using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 velocity;


    // Start is called before the first frame update
    void Start()
    {
        // sets the ridigbody2d of the iceball eqaul to its component
        //GameObject thisProjectile = gameObject;
        rb = GetComponent<Rigidbody2D>();
        velocity = rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {

        // makes iceball initially shoot in a straight line
        if(rb.velocity.y < velocity.y)
            rb.velocity = velocity;


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        rb.velocity = new Vector2(velocity.x, -velocity.y);

        Player player = col.gameObject.GetComponent<Player>(); // reference to player script
        GameObject thisProjectile = gameObject; // reference to the game object this scripts attached to

        FindObjectOfType<AudioManager>().Play("IceHit");

        if (thisProjectile.name == "FireBomb(Clone)")
        {
            if (col.collider.tag == "Player")
            {

                player.Hit();
                Explode();
                FindObjectOfType<AudioManager>().Play("IceHit");
            }
            if (col.collider.tag == "Ground")
            {
                Debug.Log("Hit!");
                Explode();
                FindObjectOfType<AudioManager>().Play("IceHit");
            }
        }
        else
        {
            // checks if iceball collides with an enemy and kills it if it does
            if (col.collider.tag == "Enemy")
            {

                Destroy(col.gameObject);
                Explode();
                FindObjectOfType<AudioManager>().Play("IceHit");
            }

            // deletes iceball when it comes in contact with a collider
            if (col.contacts[0].normal.x != 0)
            {
                Debug.Log("Hit!");
                Explode();
                FindObjectOfType<AudioManager>().Play("IceHit");
            }
        }
        /*
        // checks if iceball collides with an enemy and kills it if it does
        if (col.collider.tag=="Enemy")
        {

            Destroy(col.gameObject);
            Explode();
            FindObjectOfType<AudioManager>().Play("IceHit");
        }

        // deletes iceball when it comes in contact with a collider
        if (col.contacts[0].normal.x!=0)
        {
            Debug.Log("Hit!");
            Explode();
            FindObjectOfType<AudioManager>().Play("IceHit");
        }
        */
    }

    // function to delete game objects
    void Explode()
    {
        Destroy(this.gameObject);
    }

}
