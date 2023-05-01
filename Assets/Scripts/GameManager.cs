// Written by William Boguslawski
// following Zig tutorial

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // allows so that

    public int world { get; private set; } // public get, private set
    public int stage { get; private set; } 
    public int lives { get; private set; }
    public int coins { get; private set; }
    //private bool startPlaying = true;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // do not destroy game object when another scene is loaded
            // will maintain game manager as you pass through levels
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null; 
        }
        
    }

    private void Start()
    {
        Application.targetFrameRate = 60; // sets the target framerate to 60fps
        GameObject myObject = GameObject.Find("MyObject");
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Menu")
        {
            
        }

    }

    public void NewGame()
    {
        lives = 3;
        coins = 0;

        LoadLevel(1, 1); //creates a new game with given level and lives
    }

    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}"); // loads the given stage level
    }

    public void NextLevel()
    {

        LoadLevel(world, stage + 1);
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
        // makes it so game doesnt restart immediatly
    }

    public void ResetLevel()
    {
        lives -= 1; // decreases amount of lives by 1

        if (lives > 0) // if you die, reload level with current world and stage you are on
        {
            LoadLevel(world, stage);
        }
        else // if out of lives then game over
        {
            GameOver(); 
        }
    }

    private void GameOver()
    {
        NewGame(); // restarts the game
    }

    

    public void AddLife()
    {
        lives++;
    }

    

    
}


