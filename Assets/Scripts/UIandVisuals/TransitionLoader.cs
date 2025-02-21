using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionLoader : MonoBehaviour
{
    public static TransitionLoader Instance { get; private set;}

    private Animator animator;

    private void Awake() 
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    public void StartTransition()
    {
        animator.SetTrigger("Start");
    }
}
