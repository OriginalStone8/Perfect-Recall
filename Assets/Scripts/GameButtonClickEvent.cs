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
                SafeAreaManager.Instance.PauseMove();
                if (SafeAreaManager.Instance.CheckForSafeArea())
                {
                    SequenceInputManager.Instance.AddInputButton(btn);
                    SafeAreaManager.Instance.GenerateSafeArea();
                }
                else
                {
                    //worng click, game over
                    FindObjectOfType<AudioPlayer>().PlayGameOverSFX();
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
