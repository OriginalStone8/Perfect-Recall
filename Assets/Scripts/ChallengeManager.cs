using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    public static ChallengeManager Instance { get; private set;}

    [SerializeField] private List<ChallengeSO> challengeSOs;
    [SerializeField] private int challengeRoundDivider;

    public enum ChallengeType
    {
        ShuffledButtons,
        MirrorMode,
        SpeedUp,
        HoldToConfirm,
        DisapearingButtons,
        ButtonHell
    }

    private ChallengeSO activeChallenge;

    private void Awake() 
    {
        Instance = this;
        activeChallenge = null;
    }

    public void ResetAfterChallenge()
    {
        switch (activeChallenge.challengeType)
        {
            case ChallengeType.ShuffledButtons:
                List<GameButton> buttons = SequenceManager.Instance.GetGameButtonList();
                foreach (GameButton btn in buttons) btn.ResetToOriginalPos();
                break;
            case ChallengeType.DisapearingButtons:
                List<GameButton> buttons1 = SequenceManager.Instance.GetGameButtonList();
                foreach (GameButton btn in buttons1) btn.FadeIn(MidRoundChallenge.Instance.GetDisappearingDuration());
                break;
            case ChallengeType.SpeedUp:
                SequenceManager.Instance.SetSpeedUpShowcase(false);
                ButtonColorAssigner.Instance.SetSpeedUp(false);
                break;
            case ChallengeType.ButtonHell:
                ButtonHell.Instance.RemoveButtons();
                break;
        }
    }

    public ChallengeSO RandomChallenge()
    {
        return challengeSOs[Random.Range(0, challengeSOs.Count)];
    }

    public int GetChallengeRoundDivider()
    {
        return challengeRoundDivider;
    }

    public ChallengeSO GetActiveChallenge()
    {
        return activeChallenge;
    }

    public void SetActiveChallenge(ChallengeSO challengeSO)
    {
        activeChallenge = challengeSO;
    }

    public bool CompareActiveChallengeType(ChallengeType type)
    {
        return activeChallenge != null && activeChallenge.challengeType == type;
    }
}
