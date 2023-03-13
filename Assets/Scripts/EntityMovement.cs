using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 1f; // variable for speed
    public Vector2 direction = Vector2.left;    // variable for direction
    // public float gravity = -9.81f; allows you to customize the gravity per entity, useful for adding in the cheat menu or something

    private new Rigidbody2D rigidbody; // provides reference for rigid body in unity
    private Vector2 velocity; // variable for velocity

    private void Awake()
    {
        rigidbody = GetComponent <Rigidbody2D>();
        enabled = false;
    }

    // things dont move till the player see's them
    // this will allow entitys to move when they become visible to player
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

    // implementing actual movement
    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime; // refers to gravity that unity defines / uses
        // velocity.y += gravity; refering to custom gravity above if we wanted to change it for fun

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime); // this line actually moves the rigid body

        // flips entity direction when they run into something
        if (rigidbody.Raycast(direction))
        {
            direction = -direction;          
        }

        if (rigidbody.Raycast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
    }
}
