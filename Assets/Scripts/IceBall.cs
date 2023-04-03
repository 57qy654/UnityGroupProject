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

        if (col.collider.tag=="Enemy")
        {
            Destroy(col.gameObject);
            Explode();
        }

        // deletes iceball when it comes in contact with a collider
        if (col.contacts[0].normal.x!=0)
        {
            Explode();
        }
    }

    // function to delete game objects
    void Explode()
    {
        Destroy(this.gameObject);
    }

}
