using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;
    public static ScoreManager Instance { get; private set; }
    int lives = GameManager.Instance.lives;

    int score = 0;
    public int coins { get; private set; }

    private void Awake()
    {
        
        Instance = this;
    }

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        scoreText.text = score.ToString() + " COINS";
        lifeText.text = lives.ToString() + " LIVES";
    }

    public void AddCoin()
    {
        coins++;
        score++;
        scoreText.text = score.ToString() + " COINS";
    }
}

