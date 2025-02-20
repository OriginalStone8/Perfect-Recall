using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameButtonClickEvent : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        bool holdToConfirmActive = ChallengeManager.Instance.CompareActiveChallengeType(ChallengeManager.ChallengeType.HoldToConfirm);
        if (GameLoopManager.Instance.GetCurrentState() == GameLoopManager.State.RoundGame && !holdToConfirmActive)
        {
            GameButton btn = this.GetComponent<GameButton>();
            SequenceInputManager.Instance.AddInputButton(btn);

            if (ChallengeManager.Instance.CompareActiveChallengeType(ChallengeManager.ChallengeType.DisapearingButtons))
            {
                btn.FadeInClick();
            }
            else
            {
                btn.Pressed();
                btn.LightUp();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        bool holdToConfirmActive = ChallengeManager.Instance.CompareActiveChallengeType(ChallengeManager.ChallengeType.HoldToConfirm);
        if (holdToConfirmActive)
        {
            GameButton btn = this.GetComponent<GameButton>();
            btn.HoldClick();
            //start slider
            HoldToConfirmButton.Instance.ToggleHoldSlider(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bool holdToConfirmActive = ChallengeManager.Instance.CompareActiveChallengeType(ChallengeManager.ChallengeType.HoldToConfirm);
        if (holdToConfirmActive)
        {
            HoldToConfirmButton.Instance.ToggleHoldSlider(false);

            GameButton btn = this.GetComponent<GameButton>();
            btn.ReleaseClick();
            
            if (HoldToConfirmButton.Instance.CheckForSafeArea())
            {
                SequenceInputManager.Instance.AddInputButton(btn);
            }
            else
            {
                //worng click, game over
                SequenceManager.Instance.CallGameOver();
            }
        }
    }
}
