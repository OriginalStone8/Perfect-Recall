using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SafeAreaManager : MonoBehaviour
{
    public static SafeAreaManager Instance { get; private set; }

    [SerializeField] private SafeAreaSlider slider;
    [SerializeField] private float range;
    [SerializeField] private float speed;
    [SerializeField] private CanvasGroup canvasGroup;

    private float safeAreaMin, safeAreaMax;

    private void Awake() 
    {
        Instance = this;
    }

    public void ToggleHoldSlider(bool enable)
    {
        if (enable)
        {
            slider.ToggleSliderMove(true);
            slider.gameObject.SetActive(true);
            slider.GenerateSafeArea();
        }
        else
        {
            slider.ToggleSliderMove(false);
            Invoke("SetSliderOff", 0.2f);
        }
    }

    public void GenerateSafeArea()
    {
        slider.GenerateSafeArea();
    }

    public void PauseMove()
    {
        slider.PauseMove();
    }

    public void SetSafeArea(float min)
    {
        safeAreaMax = min + range;
        safeAreaMin = min;
    }

    private void SetSliderOff()
    {
        slider.gameObject.SetActive(false);
    } 

    public bool CheckForSafeArea()
    {
        if (slider.GetValue() >= safeAreaMin && slider.GetValue() <= safeAreaMax)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ToggleVisibility(bool enable)
    {
        //StartCoroutine(FadeVisibilityRoutine(enable, 0.3f));
        if (enable) canvasGroup.alpha = 1;
        else canvasGroup.alpha = 0;
    }

    private IEnumerator FadeVisibilityRoutine(bool enable, float time)
    {
        if (enable)
        {
            canvasGroup.alpha = 0;
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += 0.02f;
                yield return new WaitForSeconds(time/50);
            }
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.alpha = 1;
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha -= 0.02f;
                yield return new WaitForSeconds(time/50);
            }
            canvasGroup.alpha = 0;
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}
