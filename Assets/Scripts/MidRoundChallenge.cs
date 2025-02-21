using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MidRoundChallenge : MonoBehaviour
{
    public static MidRoundChallenge Instance { get; private set; }

    public event EventHandler OnMidRoundChallengeFinished;

    [SerializeField] private float buttonShuffleDuration, buttonDisapearDuration;
    [SerializeField] private LeanTweenType tweenType;


    private void Awake() 
    {
        Instance = this;
    }

    public void StartMidRoundChallenge()
    {
        ChallengeManager.ChallengeType type = ChallengeManager.Instance.GetActiveChallenge().challengeType;
        switch (type)
        {
            case ChallengeManager.ChallengeType.ShuffledButtons:
                StartCoroutine(ShuffleButtonsRoutine());
                break;
            case ChallengeManager.ChallengeType.DisapearingButtons:
                StartCoroutine(DisappearingButtonsRoutine());
                break;
        }
        //OnMidRoundChallengeFinished?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator ShuffleButtonsRoutine()
    {
        // steps:
        // 1) pair the 4 buttons
        List<GameButton> buttons = FindObjectsOfType<GameButton>().ToList();
        ListExtensions.Suffle(buttons);

        // 2) using lean tween swap one pair positions at a time
        float animDuration = buttonShuffleDuration / buttons.Count * 2;
        for (int i = 0; i < buttons.Count; i+=2)
        {
            Vector3 pos1 = buttons[i].transform.position;
            Vector3 pos2 = buttons[i+1].transform.position;

            yield return new WaitForSeconds(animDuration);

            buttons[i].transform.LeanMove(pos2, animDuration).setEase(tweenType);
            buttons[i+1].transform.LeanMove(pos1, animDuration).setEase(tweenType);

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.5f);
        OnMidRoundChallengeFinished?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator DisappearingButtonsRoutine()
    {
        // steps:
        // 1) using lean tween to fade out the buttons
        List<GameButton> buttons = FindObjectsOfType<GameButton>().ToList();

        yield return new WaitForSeconds(buttonDisapearDuration);
        for (int i = 0; i < buttons.Count; i++)
        { 
            buttons[i].FadeOut(buttonDisapearDuration);
        }

        yield return new WaitForSeconds(0.1f);
        OnMidRoundChallengeFinished?.Invoke(this, EventArgs.Empty);
    }

    public IEnumerator ShowButtonsRoutine()
    {
        // steps:
        // 1) using lean tween to fade out the buttons
        List<GameButton> buttons = FindObjectsOfType<GameButton>().ToList();

        yield return new WaitForSeconds(buttonDisapearDuration);
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].FadeIn(buttonDisapearDuration);
        }

        yield return new WaitForSeconds(0.1f);
    }

    public float GetDisappearingDuration()
    {
        return buttonDisapearDuration;
    }
}
