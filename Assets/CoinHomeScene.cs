using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinHomeScene : MonoBehaviour
{
    public TextMeshProUGUI txtCoins;
    public CoinData coinData;
    private void Start()
    {
        txtCoins.text=coinData.coin.ToString();
    }

    private void Update()
    {
        
    }
}
