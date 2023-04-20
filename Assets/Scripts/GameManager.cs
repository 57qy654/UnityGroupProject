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
    
    
    private void Update()
    {
        // creates an array of game objects that find all objects with tag "Boss" in the scene and puts in array
        GameObject[] bossSearcher = GameObject.FindGameObjectsWithTag("Boss");

        // if there are no more bosses left in the scene, load the next level
        if (bossSearcher.Length == 0)
        {
            NextLevel();
        }
    }
    

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

        NewGame(); // creates new game when its started
    }

    private void NewGame()
    {
        lives = 3;
        coins = 0;

        LoadLevel(1, 1); //creates a new game with given level and lives
    }

    private void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}"); // loads the given stage level
        //SceneManager.LoadScene(SceneManager.CurrentScene.BuildIndex); // loads the given stage level

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


