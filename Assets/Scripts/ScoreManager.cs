using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static ScoreManager Instance { get; private set; }

    int score = 0;
    public int coins { get; private set; }

    private void Awake()
    {
        
        Instance = this;
    }

    void Start()
    {
        scoreText.text = score.ToString() + " COINS";
    }

    public void AddCoin()
    {
        coins++;
        score++;
        scoreText.text = score.ToString() + " COINS";
    }
}
