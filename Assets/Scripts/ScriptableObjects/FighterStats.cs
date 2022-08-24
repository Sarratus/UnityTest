using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FighterStats")]
public class FighterStats : ScriptableObject
{
    public int maxHealth;
    public float punchCooldown;
    public int baseDamage;
}
