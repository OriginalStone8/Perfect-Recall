using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceInputManager : MonoBehaviour
{
    public static SequenceInputManager Instance { get; private set; }

    private List<GameButton> inputedSequence = new List<GameButton>();

    private void Awake() 
    {
        Instance = this;
    }

    public void AddInputButton(GameButton btn)
    {
        inputedSequence.Add(btn);
        SequenceManager.Instance.CheckInputValidity(inputedSequence);
    }

    public void ClearInputList()
    {
        inputedSequence.Clear();
    }
}
