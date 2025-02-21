using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class StartCountDown : MonoBehaviour
{
    public static StartCountDown Instance { get; private set; }

    public event EventHandler OnCountdownFinished;

    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private float countDownTime;
    private float time;
    private int prevSec = 4;

    private void Awake() 
    {
        Instance = this;
        time = countDownTime;
    }

    private void Update() 
    {
        if (GameLoopManager.Instance.GetCurrentState() == GameLoopManager.State.Countdown)
        {
            time -= Time.deltaTime;
            countDownText.text = ((int)time).ToString();
            if ((int)time < prevSec && (int)time != 0)
            {
                prevSec = (int)time;
                FindObjectOfType<AudioPlayer>().PlayCountdownSFX();
            }
        }
        
        if (GameLoopManager.Instance.GetCurrentState() == GameLoopManager.State.Countdown && time <= 0)
        {
            FindObjectOfType<AudioPlayer>().PlayCountdownCompletedSFX();
            time = countDownTime;
            OnCountdownFinished?.Invoke(this, EventArgs.Empty);
            Invoke("TurnOffCountDownText", 0.05f);
        }
    }

    private void TurnOffCountDownText()
    {
        countDownText.gameObject.SetActive(false);
    }

    public void ToggleCountdownText(bool enable)
    {
        countDownText.gameObject.SetActive(enable);
    }
}
