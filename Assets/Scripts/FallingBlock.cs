using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private Rigidbody2D blockRigidbody;

    public IEnumerator Tumble()
    {
        Debug.Log("Tumble coroutine started.");
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Earthquake");
        yield return new WaitForSecondsRealtime(2.0f);
        audioManager.Stop("Earthquake");

        //PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        //playerMovement.enabled = false;


        blockRigidbody = GetComponent<Rigidbody2D>();
        blockRigidbody.bodyType = RigidbodyType2D.Dynamic;
        blockRigidbody.gravityScale = 5.0f;
        Vector2 location = transform.position;
        location.y = location.y - 0.1f * Time.deltaTime;
        blockRigidbody.MovePosition(location);
        audioManager.Play("Waa");
        yield return new WaitForSecondsRealtime(0.5f);
        audioManager.Stop("Waa");
        Debug.Log("End of Tumble");
    }

}
