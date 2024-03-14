using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeSceneManager : MonoBehaviour
{
    public TextMeshProUGUI txtCoins;
    public TextMeshProUGUI txtLevel;
    public HealthBar expBar;
    public CoinData coinData;
    public PlayerData playerData;

    public GameObject avtCharacter;
    public GameObject player;
    public Sprite[] characterSprites;
    public string[] stringName;
    public TextMeshProUGUI txtName;
    private CharacterSelect characterSelect;
    private void Start()
    {

        characterSelect = FindObjectOfType<CharacterSelect>();
        txtCoins.text = coinData.coin.ToString();
        expBar.SetMaxHealth(playerData.maxExp);

    }

    private void Update()
    {
        txtLevel.text = playerData.currentLevel.ToString();
        expBar.SetHealth(playerData.currentExp);
        if (characterSelect != null && characterSelect.selectedCharacter >= 0 && characterSelect.selectedCharacter < characterSprites.Length)
        {
            avtCharacter.GetComponent<Image>().sprite = characterSprites[characterSelect.selectedCharacter];
            player.GetComponent<Image>().sprite = characterSprites[characterSelect.selectedCharacter];
            txtName.GetComponent<TextMeshProUGUI>().text= stringName[characterSelect.selectedCharacter];
        }

    }
}




