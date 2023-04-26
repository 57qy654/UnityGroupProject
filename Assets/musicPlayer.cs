using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class musicPlayer : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "1-1")
        {
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            audioManager.Play("FirstTheme");
        }
        if (sceneName == "1-2")
        {
            //Sound s = Array.Find(sounds, sound => sound.name == name);
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            audioManager.Play("Jungle");
        }
    }


}
