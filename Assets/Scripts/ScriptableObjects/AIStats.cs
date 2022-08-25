using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIStats")]
public class AIStats : ScriptableObject
{
    public float aiUpdatePeriod = 0.65f;
    public float punchChance = 0.4f;
    public float blockChance = 0.2f;
}
