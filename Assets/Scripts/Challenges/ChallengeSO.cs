using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New ChallengeSO", fileName = "New Challenge")]
public class ChallengeSO : ScriptableObject
{
    public string challengeName;
    public string description;
    public Sprite icon;
    public ChallengeManager.ChallengeType challengeType;
    public bool midRound;
}
