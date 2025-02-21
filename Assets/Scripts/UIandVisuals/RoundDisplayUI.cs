using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundDisplayUI : MonoBehaviour
{
    public static RoundDisplayUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private Slider roundProgressSlider;
    [SerializeField] private GameObject roundWaitPopUp;
    [SerializeField] private GameObject roundChallengeDisplay;
    [SerializeField] private TextMeshProUGUI roundWaitPopUpText;
    [SerializeField] private Button roundWaitPopUpButton;

    [SerializeField] private float popUpAnimDuration;
    [SerializeField] private float minY, maxY;
    [SerializeField] private LeanTweenType tweenType;

    private void Awake() 
    {
        Instance = this;
    }

    private void Start() 
    {
        roundWaitPopUpButton.onClick.AddListener(CallStartRound);
    }

    private void CallStartRound()
    {
        GameLoopManager.Instance.SetCurrentState(GameLoopManager.State.Countdown);
        ToggleRoundWaitPopUp(false, null);
        StartCountDown.Instance.ToggleCountdownText(true);
    }

    public void UpdateSlider(int currentInsideRound, int max)
    {
        roundProgressSlider.maxValue = max;
        roundProgressSlider.value = currentInsideRound;
    }

    public void UpdateText(int round)
    {
        roundText.text = "ROUND " + round.ToString();
    }

    public void ToggleRoundWaitPopUp(bool enable, ChallengeSO challengeSO)
    {
        roundWaitPopUpText.text = enable ? "ROUND " + GameLoopManager.Instance.GetCurrentRound().ToString() : "";
        roundWaitPopUp.SetActive(true);
        if (challengeSO != null && enable)
        {
            roundChallengeDisplay.SetActive(true);
            roundChallengeDisplay.GetComponent<ChallengeInfoUIEvent>().SetChallenge(challengeSO);
        }
        else
        {
            roundChallengeDisplay.SetActive(false);
        }
        PopUpAnim(enable);
    }

    public void ToggleRoundWaitPopUp(bool enable, ChallengeSO challengeSO, string sceneTheme)
    {
        ToggleRoundWaitPopUp(enable, challengeSO);
        SceneVisualChange.Instance.ChangeSceneColorTheme(sceneTheme);
        if (enable && challengeSO != null)
        {
            FindObjectOfType<AudioPlayer>().PlayChallengeSFX();
        }
    }

    private void PopUpAnim(bool enable)
    {
        Vector3 start;
        Vector3 target;
        if (enable)
        {
            start = new Vector3(transform.position.x, minY);
            target = new Vector3(transform.position.x, maxY);
            roundWaitPopUp.transform.position = start;
            roundWaitPopUp.transform.LeanMoveLocal(target, popUpAnimDuration).setEase(tweenType);
        }
        else
        {
            start = new Vector3(transform.position.x, maxY);
            target = new Vector3(transform.position.x, minY);
            roundWaitPopUp.transform.position = start;
            roundWaitPopUp.transform.LeanMove(target, popUpAnimDuration).setEase(tweenType).setOnComplete(() => {
                roundWaitPopUp.SetActive(false);
            });
        }
    }
}
