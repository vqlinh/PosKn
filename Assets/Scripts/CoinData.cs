using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Coin Data", menuName = "Coin Data")]

public class CoinData : ScriptableObject
{
    [HideInInspector] public int coin;
}
