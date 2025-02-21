using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChallengeInfoUIEvent : MonoBehaviour
{
    [SerializeField] private GameObject infoElement;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Image infoChallengeIcon, challengeIcon;
    [SerializeField] private TextMeshProUGUI challengeNameText;

    private ChallengeSO challengeSO;

    public void ToggleChanllangeInfo(bool enable)
    {
        if (enable)
        {
            infoText.text = challengeSO.description;
            challengeNameText.text = challengeSO.challengeName;
            infoChallengeIcon.sprite = challengeSO.icon;
        }
        infoElement.SetActive(enable);
    }

    public void SetChallenge(ChallengeSO challengeSO)
    {
        this.challengeSO = challengeSO;
        challengeIcon.sprite = challengeSO.icon;
        ChallengeManager.Instance.SetActiveChallenge(challengeSO);
    }

    public ChallengeSO GetChallenge()
    {
        return challengeSO;
    }
}
