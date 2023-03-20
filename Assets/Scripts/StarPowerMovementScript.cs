using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPowerMovementScript : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left;

    private new Rigidbody2D rigidbody;
    private Vector2 velocity;
    private bool isBouncing = false;
    public float bounceForce = 50f;
    public float maxBounceHeight = 10f;
    private float startingY;
    private float gravity = 9.8f;

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

    private void OnEnable()
    {
        rigidbody.WakeUp();
        startingY = transform.position.y;
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
            velocity.x = direction.x * speed;
            velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

            rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);

            if (rigidbody.Raycast(Vector2.down))
            {
                isBouncing = true;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f); // reset vertical velocity to 0 before applying bounce force
                rigidbody.AddForce(new Vector2(0f, bounceForce), ForceMode2D.Impulse); // apply bounce force in opposite direction of current velocity
            }
        }
        else
        {
            float height = transform.position.y - startingY;
            if (height < maxBounceHeight)
            {
                float bounceVelocity = Mathf.Sqrt(2f * bounceForce * (maxBounceHeight - height));
                velocity.y = bounceVelocity;
            }
            else
            {
                isBouncing = false;
            }
        }

        Debug.Log("Velocity: " + velocity);
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
        }
    }
}
