using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HoldToConfirmButton : MonoBehaviour
{
    public static HoldToConfirmButton Instance { get; private set; }

    [SerializeField] private HoldSlider slider;
    [SerializeField] private float range;
    [SerializeField] private float speed;

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

    public float GetSpeed()
    {
        return speed;
    }
}
