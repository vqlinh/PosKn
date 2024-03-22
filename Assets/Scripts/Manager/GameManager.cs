using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public TextMeshProUGUI txtCoins;
    public int coin;
    public CoinData coinData;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadCoin();
    }

    private void Update()
    {
        txtCoins.text = coinData.coin.ToString();

    }

    public void SaveCoin()
    {
        coinData.coin = Instance.coin;
    }

    public void LoadCoin()
    {
        if (coinData == null)
            coinData = ScriptableObject.CreateInstance<CoinData>();

        if (coinData != null)
            Instance.coin = coinData.coin;

    }
}

