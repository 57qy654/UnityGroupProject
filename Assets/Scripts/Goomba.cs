using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;

    // checks collision of goomba and player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // checks what goomba collides with
        {
            if (collision.transform.DotTest(transform, Vector2.down)) // checks if player lands on goomba head
            {
                Flatten();
            }
        }
    }

    // flatten method / death of goomba
    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false; // disables goomba collider
        GetComponent<EntityMovement>().enabled = false; // disables goomba movement
        GetComponent<AnimatedSprite>().enabled = false; // disables goomba animations
        GetComponent<SpriteRenderer>().sprite = flatSprite; // updates sprite to flat goomba
        Destroy(gameObject, 0.5f); // destroys goomba after half a second, so you are able to see flat goomba
    }
}