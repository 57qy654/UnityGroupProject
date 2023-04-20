using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummy : MonoBehaviour
{
    private GameManager gameManager;  // add a reference to the game manager
    public Sprite flatSprite;

    // Start is called before the first frame update
    void Start()
    {
        // get the reference to game manager script to have it change levels when boss dies.
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // checks what goomba collides with
        {
            Player player = collision.gameObject.GetComponent<Player>(); // create reference to player script so you can call the player to get hit

            if (collision.transform.DotTest(transform, Vector2.down)) // checks if player lands on goomba head
            {
                Flatten();
                gameManager.NextLevel();
            }

        }

    }

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false; // disables goomba collider
        GetComponent<EntityMovement>().enabled = false; // disables goomba movement
        GetComponent<AnimatedSprite>().enabled = false; // disables goomba animations
        GetComponent<SpriteRenderer>().sprite = flatSprite; // updates sprite to flat goomba
        Destroy(gameObject, 0.5f); // destroys goomba after half a second, so you are able to see flat goomba
    }
}
