using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterStats : ScriptableObject
{
    public int maxHealth { get; private set; }
    public float punchCooldown { get; private set; }
    public int baseDamage { get; private set; }
}
