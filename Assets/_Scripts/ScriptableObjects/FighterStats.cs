using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FighterStats")]
public class FighterStats : ScriptableObject
{
    [Header("Stats")]
    public int maxHealth = 100;
    public int baseDamage;

    [Header("Death Parameters")]
    public float resurrectionTimerDuration = 5f;
    public float resurrectionChance;
}
