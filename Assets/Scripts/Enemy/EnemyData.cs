using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy Data")]

public class EnemyData : ScriptableObject
{
    public int health;
    public int attack;
    public int exp;
}
