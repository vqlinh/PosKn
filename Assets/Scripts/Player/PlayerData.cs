using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]

public class PlayerData : ScriptableObject
{
    public int maxExp;
    public int currentExp;
    public int currentLevel;
    public int maxHealth;
    
}
