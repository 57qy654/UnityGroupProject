// Written by Jude Pitschka

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaBall : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 initialVelocity;
    public float jumpForce = 10f;
    public float jumpHeight = 2f;
    public float fallGravityScale = 2f;

    private Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        // sets the ridigbody2d of the iceball eqaul to its component
        rb = GetComponent<Rigidbody2D>();
        initialVelocity = rb.velocity;
        startPosition = transform.position;

        // set initial velocity to jump up
        rb.velocity = new Vector2(initialVelocity.x, jumpForce);
    }

    // Update is called once per frame
    void Update()
    {
        // if the ball has fallen back to its initial position, jump up again
        if (transform.position.y <= startPosition.y)
        {
            rb.velocity = new Vector2(initialVelocity.x, jumpForce);
            rb.gravityScale = 1f;
        }

        // if the ball is at its peak height, switch to falling gravity scale
        if (transform.position.y >= startPosition.y + jumpHeight)
        {
            rb.gravityScale = fallGravityScale;
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // checks if iceball collides with an enemy and kills it if it does
        if (col.collider.tag == "Player")
        {

            Player player = col.gameObject.GetComponent<Player>();

            if (!player.starpower)
                player.Hit();

        }

       
    }
}
