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
        else
        {
            textField.text = "";
        }
    }
}
