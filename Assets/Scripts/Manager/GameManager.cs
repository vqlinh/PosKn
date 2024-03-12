using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public TextMeshProUGUI txtMaxHeal;
    public TextMeshProUGUI txtCurrentHeal;
    public TextMeshProUGUI coins;
    public int coin;

    private const string CoinKey = "PlayerCoin";

    private void Start()
    {
        LoadCoin();
    }

    private void Update()
    {
        coins.text = coin.ToString();
    }

    private void OnDestroy()
    {
        SaveCoin();
    }

    public void SaveCoin()
    {
        PlayerPrefs.SetInt(CoinKey, coin);
        PlayerPrefs.Save();
    }

    public void LoadCoin()
    {
        if (PlayerPrefs.HasKey(CoinKey)) coin = PlayerPrefs.GetInt(CoinKey);
    }
}
