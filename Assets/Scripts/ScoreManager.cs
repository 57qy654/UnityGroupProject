// Written by Jude Pitschka

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI levelText;
    public static ScoreManager Instance { get; private set; }
    int lives; //= GameManager.Instance.lives;
    int stage; //= GameManager.Instance.stage;

    int score = 0;
    public int coins { get; private set; }

    private void Awake()
    {
        
        Instance = this;
    }

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        lives = gameManager.lives;
        stage = gameManager.stage;

        scoreText.text = "COINS: " + score.ToString();
        lifeText.text = "LIVES: " + lives.ToString();
        levelText.text = "LEVEL: " + stage.ToString();
    }

    public void AddCoin()
    {
        coins++;
        score++;
        scoreText.text = score.ToString() + " COINS";
    }
}

