using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// same death animation for all entitys
public class DeathAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // a refernce to sprite renderer that want to update
    public Sprite deadSprite; // changes what sprite is active for given entity

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // when death animation is enabled
    private void OnEnable()
    {
        UpdateSprite();                                         // start at 53:25 on video
        DisablePhysics();
        StartCoroutine(Animate());
    }


    private void UpdateSprite()
    {
        spriteRenderer.enabled = true; // enables sprite renderer
        spriteRenderer.sortingOrder = 10; 

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = deadSprite;
        }
    }

    // disables colliders, rigid bodys, movement etc.
    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>(); // returns an array of collider 2D

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false; // makes sure the enemy falls through ground when dead
        }

        GetComponent<Rigidbody2D>().isKinematic = true; // turns off rigid body and makes sure physics engine isnt affecting object

        // disables custom movement scripts
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        if (entityMovement != null)
        {
            entityMovement.enabled = false;
        }
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;     // how much time has elapsed
        float duration = 3f;

        float jumpVelocity = 10f; // how fast enemies jump and fall off when dead
        float gravity = -36f;

        Vector3 velocity = Vector3.up * jumpVelocity;   // establish velocity vector

        while (elapsed < duratiion) // while time is < duration continue animating
        {
            transform.position += velocity * Time.deltaTime; // changing position over time
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

}
