using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeSceneManager : MonoBehaviour
{
    public HealthBar expBar;
    //public CoinData coinData;
    public PlayerData playerData;
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
    }

    private void Update()
    {
        txtLevel.text = playerData.currentLevel.ToString();
        expBar.SetHealth(playerData.currentExp);
        if (characterSelect != null && characterSelect.characterSelect >= 0 && characterSelect.characterSelect < avataCharacterSprites.Length)
        {
            avtCharacter.GetComponent<Image>().sprite = avataCharacterSprites[characterSelect.characterSelect];
            player.GetComponent<Image>().sprite = playerCharacterSprites[characterSelect.characterSelect];
            txtName.GetComponent<TextMeshProUGUI>().text = stringName[characterSelect.characterSelect];
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(i == characterSelect.characterSelect - 1);
            if (PlayerPrefs.GetInt("Character_" + (i + 1) + "_Bought", 0) == 1)
            {
                buttons[i].SetActive(false);
            }
        }
    }
    public void BuyCharacter(int characterIndex)
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
        int priceCharacter = 2000; 
        if (GameManager.Instance.coin >= priceCharacter)
        {
            GameManager.Instance.coin -= priceCharacter;
            GameManager.Instance.SaveCoin();
            PlayerPrefs.SetInt("Character_" + (characterIndex) + "_Bought", 1);
            buttons[characterIndex].SetActive(false); 
        }
    }



}




