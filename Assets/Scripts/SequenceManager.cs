using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public static SequenceManager Instance { get; private set; }

    public event EventHandler OnShowcaseFinished;
    public event EventHandler OnShowcaseStarts;
    public event EventHandler OnGameOver;

    private List<GameButton> requiredActionSequence = new List<GameButton>();

    private List<GameButton> gameButtons = new List<GameButton>();

    private float timeToCompleteSeq;
    private bool countTime;
    private bool speedUp;

    private void Awake() 
    {
        Instance = this;
        
        List<GameButton> buttonList = FindObjectsOfType<GameButton>().ToList();
        gameButtons.Clear();
        for (int i = 0; i < buttonList.Count; i++)
        {
            if (!buttonList[i].GetIsUI())
                gameButtons.Add(buttonList[i]);
        }
    }
    

    private void Start() 
    {
        StartCountDown.Instance.OnCountdownFinished += ExpandSequenceEvent;
    }

    private void Update() 
    {
        if (countTime)
        {
            timeToCompleteSeq += Time.deltaTime;
        }
    }

    public void ExpandSequenceEvent(object sender, System.EventArgs e)
    {
        AddActionButton();
    }

    public void ExpandSequence()
    {
        OnShowcaseStarts?.Invoke(this, EventArgs.Empty);
        AddActionButton();
    }

    private void AddActionButton()
    {
        if (!ChallengeManager.Instance.CompareActiveChallengeType(ChallengeManager.ChallengeType.ButtonHell))
        {
            int index = UnityEngine.Random.Range(0, gameButtons.Count);
            requiredActionSequence.Add(gameButtons[index]);
        }
        else
        {
            List<GameButton> includeNewButtons = new List<GameButton>();
            for (int i = 0; i < gameButtons.Count; i++)
            {
                includeNewButtons.Add(gameButtons[i]);
            }
            for (int i = 0; i < ButtonHell.Instance.GetNewButtons().Count; i++)
            {
                includeNewButtons.Add(ButtonHell.Instance.GetNewButtons()[i]);
            }

            int index = UnityEngine.Random.Range(0, includeNewButtons.Count);
            requiredActionSequence.Add(includeNewButtons[index]);
        }
        StartCoroutine(ShowcaseSequence());
    }

    public void ResetSequence()
    {
        requiredActionSequence.Clear();
    }

    private IEnumerator ShowcaseSequence()
    {
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < requiredActionSequence.Count; i++)
        {
            requiredActionSequence[i].Pressed();
            requiredActionSequence[i].LightUp();
            float timeBetween = ButtonColorAssigner.LightUpDuration * 2;
            if (speedUp)
                timeBetween *= 0.65f;
            yield return new WaitForSeconds(timeBetween);
        }
        OnShowcaseFinished?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(0.3f);
        timeToCompleteSeq = 0;
        countTime = true;
    }

    public void CheckInputValidity(List<GameButton> input)
    {
        bool wrong = false;
        if (input.Count > requiredActionSequence.Count)
        {
            //game over
            FindObjectOfType<AudioPlayer>().PlayGameOverSFX();
            OnGameOver?.Invoke(this, EventArgs.Empty);
            return;
        }

        for (int i = 0; i < input.Count; i++)
        {
            if (requiredActionSequence[i] != input[i])
            {
                wrong = true;
            }
        }

        if (wrong)
        {
            // game over
            FindObjectOfType<AudioPlayer>().PlayGameOverSFX();
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
        else if (input.Count == requiredActionSequence.Count)
        {
            //FindObjectOfType<AudioPlayer>().PlayRoundCompletedSFX();

            countTime = false;
            
            SequenceInputManager.Instance.ClearInputList();

            //move to next inside round / expand required actions
            GameLoopManager.Instance.FinishInsideRound();
            
            int pointsToAdd = GameLoopManager.Instance.GetCurrentRound() * ScoreManager.Instance.GetScoreMultiplier();
            pointsToAdd *= Mathf.RoundToInt(ScoreManager.Instance.GetTimeBonus(timeToCompleteSeq, GameLoopManager.Instance.GetCurrentRound()));
            
            ScoreManager.Instance.ModifyScore(pointsToAdd);
        }
        else
        {
            //hasn't completed full sequence but hasn't wornged
            ScoreManager.Instance.ModifyScore(ScoreManager.Instance.GetScoreMultiplier());
        }
    }

    public void UpdateGameButtonList()
    {
        List<GameButton> buttonList = FindObjectsOfType<GameButton>().ToList();
        gameButtons.Clear();
        for (int i = 0; i < buttonList.Count; i++)
        {
            if (!buttonList[i].GetIsUI())
                gameButtons.Add(buttonList[i]);
        }
    }

    public List<GameButton> GetGameButtonList()
    {
        return gameButtons;
    }

    public void SetSpeedUpShowcase(bool enable)
    {
        speedUp = enable;
    }

    public void CallGameOver()
    {
        OnGameOver?.Invoke(this, EventArgs.Empty);
    }
}
