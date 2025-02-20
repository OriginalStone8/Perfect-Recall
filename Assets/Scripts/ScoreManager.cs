using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private int scoreMultiplier;
    [SerializeField] private int timeMultiplierMax;
    private int currentScore;

    private void Start() 
    {
        currentScore = 0;
    }

    public void ModifyScore(int score)
    {
        currentScore += score;
        ScoreDisplayUI.Instance.SetScoreText();
        CheckForHighScore();
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    public int GetScoreMultiplier()
    {
        return scoreMultiplier;
    }

    public float GetTimeBonus(float time, int currentRound)
    {
        int roundTime = Mathf.RoundToInt(time);
        float points = 0;
        switch (roundTime)
        {
            case 0:
                points = timeMultiplierMax * currentRound;
                break;
            case 1:
                points = timeMultiplierMax * currentRound * 0.6f;
                break;
        }
        return points;
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void CheckForHighScore()
    {
        if (currentScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            ScoreDisplayUI.Instance.SetHighScoreText();
        }
    }
}
