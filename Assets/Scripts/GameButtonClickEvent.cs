using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameButtonClickEvent : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameLoopManager.Instance.GetCurrentState() == GameLoopManager.State.RoundGame)
        {
            FindObjectOfType<AudioPlayer>().PlayButtonSFX();

            bool holdToConfirmActive = ChallengeManager.Instance.CompareActiveChallengeType(ChallengeManager.ChallengeType.SafeArea);
    
            GameButton btn = this.GetComponent<GameButton>();

            if (holdToConfirmActive)
            {
                HoldToConfirmButton.Instance.PauseMove();
                if (HoldToConfirmButton.Instance.CheckForSafeArea())
                {
                    SequenceInputManager.Instance.AddInputButton(btn);
                    HoldToConfirmButton.Instance.GenerateSafeArea();
                }
                else
                {
                    //worng click, game over
                    SequenceManager.Instance.CallGameOver();
                }
            }
            else
            {
                SequenceInputManager.Instance.AddInputButton(btn);
            }

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
}
