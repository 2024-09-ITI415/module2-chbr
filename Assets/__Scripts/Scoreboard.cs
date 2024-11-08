using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro text
    private int scoreCounter;
    // Start is called before the first frame update
    void Start()
    {
        scoreCounter = 0;
        UpdateScore();
    }

    public void AddScore(int newScore)
    {
        scoreCounter += newScore;
        UpdateScore();
    }
    void UpdateScore()
    {
        scoreText.text = "Score: " + scoreCounter;
    }
}
