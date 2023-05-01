// Written by William Boguslawski

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            FindObjectOfType<AudioManager>().Play("Mario Waa 1");


        }
    }

}
