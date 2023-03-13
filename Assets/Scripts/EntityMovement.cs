using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 1f; // variable for speed
    public Vector2 direction = Vector2.left;    // variable for direction

    private new Rigidbody2D rigidbody; // provides reference for rigid body in unity
    private Vector2 velocity; // variable for velocity

    private void Awake()
    {
        rigidbody = GetComponent <Rigidbody2D>(); 
    }

    // things dont move till the player see's them
    // this will allow entitys to move when they become visible to player
    private void OnBecameVisible() 
    {

    }

    private void OnBecameInvisible()
    {

    }

}
