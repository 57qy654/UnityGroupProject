// Written by Jude Pitschka

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameManager.NewGame();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
