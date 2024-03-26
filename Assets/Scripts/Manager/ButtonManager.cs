using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button skillButton1;
    public Button skillButton2;
    public Button skillButton3;

    private Color normalColor;
    public Color disabledColor;
    private TextMeshProUGUI txtCoins;


    private void Start()
    {
        normalColor = skillButton1.colors.normalColor;
        txtCoins = GameObject.Find("TxtCoin").GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        txtCoins.text = GameManager.Instance.coin.ToString();

    }

    public void DisableButtons()
    {
        skillButton1.interactable = false;
        skillButton2.interactable = false;
        skillButton3.interactable = false;

        ChangeButtonColor(disabledColor);
    }

    public void EnableButtons()
    {
        skillButton1.interactable = true;
        skillButton2.interactable = true;
        skillButton3.interactable = true;

        ChangeButtonColor(normalColor);
    }

    private void ChangeButtonColor(Color color)
    {
        ColorBlock buttonColors = skillButton1.colors;
        buttonColors.normalColor = color;
        skillButton1.colors = buttonColors;

        buttonColors = skillButton2.colors;
        buttonColors.normalColor = color;
        skillButton2.colors = buttonColors;

        buttonColors = skillButton3.colors;
        buttonColors.normalColor = color;
        skillButton3.colors = buttonColors;
    }
}
