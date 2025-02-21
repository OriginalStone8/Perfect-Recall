using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    public static GameLoopManager Instance { get; private set; }

    public enum State
    {
        Countdown,
        RoundWait,
        Showcase,
        RoundGame,
        MidRoundChallenge,
        GameOver
    }

    private State state;

    [SerializeField] private int maxPossibleSequence;

    private int currentRound = 1;
    private int currentInsideRound = 0;
    private int currentInsideRoundMax = 4;

    private void Awake() 
    {
        Instance = this;
    }

    private void Start() 
    {
        state = State.RoundWait;
        RoundDisplayUI.Instance.ToggleRoundWaitPopUp(true, null);
        SceneVisualChange.Instance.ChangeSceneColorTheme("light");
        GameStateGuideText.Instance.SetTextMessage(state);
        StartCountDown.Instance.OnCountdownFinished += OnCountdownFinished;
        SequenceManager.Instance.OnShowcaseFinished += OnShowcaseFinished;
        SequenceManager.Instance.OnShowcaseStarts += OnShowcaseStarts;
        SequenceManager.Instance.OnGameOver += OnGameOver;
        MidRoundChallenge.Instance.OnMidRoundChallengeFinished += OnMidRoundChallengeFinished;
    }

    private void Update() 
    {
        switch (state)
        {
            case State.RoundWait:
                break;
            case State.Countdown:
                break;
            case State.Showcase:
                break;
            case State.RoundGame:
                break;
            case State.GameOver:
                break;   
        }
    }

    private void OnCountdownFinished(object sender, EventArgs e)
    {
        if (ChallengeManager.Instance.CompareActiveChallengeType(ChallengeManager.ChallengeType.SafeArea))
        {
            HoldToConfirmButton.Instance.ToggleHoldSlider(true);
        }
        state = State.Showcase;
        GameStateGuideText.Instance.SetTextMessage(state);
    }

    private void OnShowcaseFinished(object sender, EventArgs e)
    {
        if (ChallengeManager.Instance.GetActiveChallenge() != null && ChallengeManager.Instance.GetActiveChallenge().midRound)
        {
            state = State.MidRoundChallenge;
            //invoke to start the mid round action
            Invoke("CallMidRoundChallenge", 0.2f);
        }
        else 
            state = State.RoundGame;
        GameStateGuideText.Instance.SetTextMessage(state);
    }

    private void CallMidRoundChallenge()
    {
        MidRoundChallenge.Instance.StartMidRoundChallenge();
    }

    private void OnShowcaseStarts(object sender, EventArgs e)
    {
        if (ChallengeManager.Instance.CompareActiveChallengeType(ChallengeManager.ChallengeType.DisapearingButtons))
        {
            StartCoroutine(MidRoundChallenge.Instance.ShowButtonsRoutine());
        }

        state = State.Showcase;
        GameStateGuideText.Instance.SetTextMessage(state);
    }

    private void OnMidRoundChallengeFinished(object sender, EventArgs e)
    {
        state = State.RoundGame;
        GameStateGuideText.Instance.SetTextMessage(state);
    }

    private void OnGameOver(object sender, EventArgs e)
    {
        state = State.GameOver;
        SceneLoader.Instance.LoadGameOverScene();
    }

    public void FinishInsideRound()
    {
        currentInsideRound++;
        RoundDisplayUI.Instance.UpdateSlider(currentInsideRound, currentInsideRoundMax);
        if (currentInsideRound == currentInsideRoundMax)
        {
            Invoke("MoveToNextRound", 0.3f);
        }
        else
        {
            Invoke("CallExpandSequence", 0.2f);
        }
    }

    private void CallExpandSequence()
    {
        SequenceManager.Instance.ExpandSequence();
    }

    private void MoveToNextRound()
    {
        FindObjectOfType<AudioPlayer>().PlayRoundCompletedSFX();

        if (ChallengeManager.Instance.GetActiveChallenge() != null)
        {
            ChallengeManager.Instance.ResetAfterChallenge();
        }

        ScoreManager.Instance.ModifyScore((int)Mathf.Pow(currentRound, 2) * ScoreManager.Instance.GetScoreMultiplier());

        currentRound++;
        currentInsideRound = 0;
        if (currentRound % 3 == 0 && currentInsideRoundMax != maxPossibleSequence)
            currentInsideRoundMax += 2;

        RoundDisplayUI.Instance.UpdateSlider(currentInsideRound, currentInsideRoundMax);
        RoundDisplayUI.Instance.UpdateText(currentRound);

        state = State.RoundWait;
        GameStateGuideText.Instance.SetTextMessage(state);

        if (currentRound % ChallengeManager.Instance.GetChallengeRoundDivider() == 0)
        {
            RoundDisplayUI.Instance.ToggleRoundWaitPopUp(true, ChallengeManager.Instance.RandomChallenge(), "dark");
            FindObjectOfType<AudioPlayer>().PlayChallengeSFX();
        }  
        else
        {
            RoundDisplayUI.Instance.ToggleRoundWaitPopUp(true, null, "light");
            ChallengeManager.Instance.SetActiveChallenge(null);
        }

        SequenceManager.Instance.ResetSequence();

        if (ChallengeManager.Instance.CompareActiveChallengeType(ChallengeManager.ChallengeType.ButtonHell))
        {
            ButtonHell.Instance.AddButtons();
        }

        if (ChallengeManager.Instance.CompareActiveChallengeType(ChallengeManager.ChallengeType.SpeedUp))
        {
            ButtonColorAssigner.Instance.SetSpeedUp(true);
        }
    }

    private void ExpandPostInsideRound()
    {
        SequenceManager.Instance.ExpandSequence();
    }

    public State GetCurrentState()
    {
        return state;
    }

    public void SetCurrentState(State state)
    {
        this.state = state;
        if (state == State.Countdown) 
        {
            StartCountDown.Instance.StartCountdown();
        }
    }

    public int GetCurrentInsideRoundMax()
    {
        return currentInsideRoundMax;
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }
}
