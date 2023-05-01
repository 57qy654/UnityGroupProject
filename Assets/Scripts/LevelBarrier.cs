// Written by William Boguslawski

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBarrier : MonoBehaviour
{
    public int nextWorld = 1;
    public int nextStage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            audioManager.Stop("Jungle");
            other.gameObject.SetActive(false);
            GameManager.Instance.LoadLevel(nextWorld, nextStage);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
