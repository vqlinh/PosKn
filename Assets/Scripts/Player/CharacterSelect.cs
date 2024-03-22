using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] skins;
    public int characterSelect;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);


        characterSelect = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject player in skins)
            player.SetActive(false);

        skins[characterSelect].SetActive(true);

    }

    public void Next()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
        skins[characterSelect].SetActive(false);
        characterSelect++;
        if (characterSelect == skins.Length) characterSelect = 0;

        skins[characterSelect].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", 0);


        if (PlayerPrefs.GetInt("Character_" + (characterSelect ) + "_Bought", 0) == 1)
        {
            PlayerPrefs.SetInt("SelectedCharacter", characterSelect);
        }
    }
    public void Back()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
        skins[characterSelect].SetActive(false);
        characterSelect--;
        if (characterSelect == -1) characterSelect = skins.Length-1;

        skins[characterSelect].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", 0);

        if (PlayerPrefs.GetInt("Character_" + (characterSelect) + "_Bought", 0) == 1)
        {
            PlayerPrefs.SetInt("SelectedCharacter", characterSelect);
        }
    }
}
