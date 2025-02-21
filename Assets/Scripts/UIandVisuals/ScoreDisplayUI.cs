using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplayUI : MonoBehaviour
{
    public static ScoreDisplayUI Instance { get; private set; }
    enum ScoreDisplayScene
    {
        GameOver,
        MainMenu,
        GameScene
    }

    [SerializeField] private ScoreDisplayScene displayScene; 
    [SerializeField] private TextMeshProUGUI highScoreText, scoreText;
    [SerializeField] private TextMeshProUGUI newHighScoreText;

    private void Awake() 
    {
        Instance = this;
    }

    private void Start() 
    {
        if (displayScene != ScoreDisplayScene.MainMenu)
            SetBothTexts();
        else
            SetHighScoreText();

        if (displayScene == ScoreDisplayScene.GameOver && ScoreManager.Instance.GetIsNewHighScore())
        {
            newHighScoreText.text = "NEW HIGH SCORE!";
        }
        else if (displayScene == ScoreDisplayScene.GameOver)
        {
            newHighScoreText.text = "";
        }
    }

    public void SetScoreText()
    {
        scoreText.text = ScoreManager.Instance.GetScore().ToString();
    }

    public void SetHighScoreText()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void SetBothTexts()
    {
        scoreText.text = ScoreManager.Instance.GetScore().ToString();
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
