using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPlayer : MonoBehaviour
{
    private GameManager gameManager;
    public AudioClip desiredClip;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (gameManager.world == 1 && gameManager.stage == 1)
        {
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            audioManager.Play("FirstTheme");
        }
    }


}
