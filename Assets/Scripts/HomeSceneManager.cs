using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeSceneManager : MonoBehaviour
{
    public HealthBar expBar;
    public CoinData coinData;
    public PlayerData playerData;
    public TextMeshProUGUI txtCoins;
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtName;

    public GameObject player;
    public GameObject avtCharacter;
    public GameObject[] buttons;
    public Sprite[] avataCharacterSprites;
    public Sprite[] playerCharacterSprites;
    public string[] stringName;
    private CharacterSelect characterSelect;
    public bool isBought = true;
    private void Start()
    {
        isBought = true;
        characterSelect = FindObjectOfType<CharacterSelect>();
        expBar.SetMaxHealth(playerData.maxExp);
        //PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        txtCoins.text = coinData.coin.ToString();
        txtLevel.text = playerData.currentLevel.ToString();
        expBar.SetHealth(playerData.currentExp);
        if (characterSelect != null && characterSelect.selectedCharacter >= 0 && characterSelect.selectedCharacter < avataCharacterSprites.Length)
        {
            avtCharacter.GetComponent<Image>().sprite = avataCharacterSprites[characterSelect.selectedCharacter];
            player.GetComponent<Image>().sprite = playerCharacterSprites[characterSelect.selectedCharacter];
            txtName.GetComponent<TextMeshProUGUI>().text = stringName[characterSelect.selectedCharacter];
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(i == characterSelect.selectedCharacter - 1);
            if (PlayerPrefs.GetInt("Character_" + (i + 1) + "_Bought", 0) == 1)
            {
                buttons[i].SetActive(false); // Ẩn nút mua khi nhân vật đã được mua
            }
        }
    }
    public void BuyCharacter(int characterIndex)
    {
        int characterCost = 2000; // Giá của mỗi nhân vật

        // Kiểm tra số tiền đủ để mua nhân vật hay không
        if (coinData.coin >= characterCost)
        {
            coinData.coin -= characterCost; // Trừ tiền khi mua nhân vật
            PlayerPrefs.SetInt("Character_" + (characterIndex + 1) + "_Bought", 1); // Lưu trạng thái đã mua của nhân vật
            buttons[characterIndex].SetActive(false); // Ẩn nút mua của nhân vật đã mua
        }
    }

    public void BuyCoin1()
    {
        coinData.coin += 2000;
    }

    public void BuyCoin2()
    {
        coinData.coin += 6000;
    }

    public void BuyCoin3()
    {
        coinData.coin += 9500;
    }


}




