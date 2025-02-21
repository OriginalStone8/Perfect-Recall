using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStateGuideText : MonoBehaviour
{
    public static GameStateGuideText Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private string showcaseM = "Showcasing sequence.. watch carefully";
    [SerializeField] private string repeatM = "Repeat the sequence..";

    private void Awake() 
    {
        Instance = this;
    }

    public void SetTextMessage(GameLoopManager.State state)
    {
        if (state == GameLoopManager.State.Showcase)
        {
            textField.text = showcaseM;
        }
        else if (state == GameLoopManager.State.RoundGame)
        {
            textField.text = repeatM;
        }
        else if (state == GameLoopManager.State.MidRoundChallenge)
        {
            if (ChallengeManager.Instance.CompareActiveChallengeType(ChallengeManager.ChallengeType.ShuffledButtons))
            {
                textField.text = "Shuffling..";
            }
            else if (ChallengeManager.Instance.CompareActiveChallengeType(ChallengeManager.ChallengeType.DisapearingButtons))
            {
                textField.text = "Disappearing..";
            }
        }
        else
        {
            textField.text = "";
        }
    }
}
