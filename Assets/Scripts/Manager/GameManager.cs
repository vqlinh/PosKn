using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int coin;
    //public CoinData coinData;

    private const string Coin = "Coin";


    private void Start()
    {
        LoadCoin();
        //coinData.coin = coin;
    }

    private void Update()
    {
    }

    public void SaveCoin()
    {
        PlayerPrefs.SetInt(Coin, coin);
        PlayerPrefs.Save();
    }

    public void LoadCoin()
    {
        if (PlayerPrefs.HasKey(Coin))
        {
            coin = PlayerPrefs.GetInt(Coin);
        }
    }
}
