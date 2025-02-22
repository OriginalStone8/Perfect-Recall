using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartCountDown : MonoBehaviour
{
    public static StartCountDown Instance { get; private set; }

    public event EventHandler OnCountdownFinished;

    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private float countDownTime;
    private float time;

    private void Awake() 
    {
        Instance = this;
        time = countDownTime;
    }

    private void Update() 
    {
        if (GameLoopManager.Instance.GetCurrentState() == GameLoopManager.State.Countdown)
        {
            int prev = (int)time;
            time -= Time.deltaTime;
            int cur = (int)time;

            PlayCountdownSound(prev, cur);
            countDownText.text = ((int)time).ToString();
        }
        
        if (GameLoopManager.Instance.GetCurrentState() == GameLoopManager.State.Countdown && time <= 0)
        {
            time = countDownTime;
            OnCountdownFinished?.Invoke(this, EventArgs.Empty);
            Invoke("TurnOffCountDownText", 0.05f);
        }
    }

    public void StartCountdown() 
    {
        PlayCountdownSound(4, 3);
    }

    private void PlayCountdownSound(int prev, int cur)
    {
        if (prev != cur)
        {
            if (cur == 0)
                FindObjectOfType<AudioPlayer>().PlayCountdownCompletedSFX();
            else
                FindObjectOfType<AudioPlayer>().PlayCountdownSFX(); 
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
