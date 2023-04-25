    using System.Collections;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;
    using Unity.VisualScripting;
    using UnityEngine;

public class StarPowerMovementScript : MonoBehaviour
{
    // sets the speed
    public float speed = 1f;
    // The direction in which the object moves
    public Vector2 direction = Vector2.left;

    private new Rigidbody2D rigidbody;
    private Vector2 velocity;
    private bool isBouncing = false;
    public float bounceForce = 50f;
    public float maxBounceHeight = 10f;
    private float startingY;
    private float gravity = 9.8f;
    private float groundLevel;

    private void Awake()
    {
        // Get the Rigidbody2D component and disable this script on awake
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnBecameVisible()
    {
        // Enables script when the object becomes visible
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        // Disables script when the object becomes invisible
        enabled = false;
    }

    private void OnEnable()
    {

        rigidbody.WakeUp();
        startingY = transform.position.y;
        groundLevel = startingY - maxBounceHeight;
    }

    private void OnDisable()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }

    private void FixedUpdate()
    {
        if (!isBouncing)
        {
            // If the object is not currently bouncing, calculate its velocity based on its speed, direction, and the acceleration due to gravity
            velocity.x = direction.x * speed;
            velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

            // Move the object based on its current velocity
            rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);

            // If the object is colliding with the ground, set isBouncing to true and apply a bounce force to the object
            if (rigidbody.Raycast(Vector2.down))
            {
                isBouncing = true;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f); // reset vertical velocity to 0 before applying bounce force
                rigidbody.AddForce(new Vector2(0f, bounceForce), ForceMode2D.Impulse); // apply bounce force in opposite direction of current velocity
            }
        }
        else
        {
            // If the object is currently bouncing, calculate its bounce velocity based on the maximum bounce height and the force applied to it
            float distanceFromGround = Mathf.Abs(transform.position.y - groundLevel);
            if (distanceFromGround < maxBounceHeight)
            {
                float bounceVelocity = Mathf.Sqrt(2f * bounceForce * (maxBounceHeight - distanceFromGround));
                velocity.y = bounceVelocity;
            }
            else
            {
                // If the object has reached its maximum bounce height, set isBouncing to false
                isBouncing = false;
            }
        }

        //Debug.Log("Velocity: " + velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Calculate bounce velocity
            float bounceVelocity = Mathf.Sqrt(2f * bounceForce * gravity * maxBounceHeight);
            Vector2 newVelocity = new Vector2(velocity.x, bounceVelocity);

            // Apply bounce force
            rigidbody.velocity = newVelocity;
            rigidbody.AddForce(new Vector2(0f, bounceForce), ForceMode2D.Impulse);

            // Set isBouncing flag to true
            isBouncing = true;

            // Calculate the ground level based on the current position and the maximum bounce height
            groundLevel = transform.position.y - maxBounceHeight;

            // Reset the starting Y position to the current position
            startingY = transform.position.y;
        }
    }

}






