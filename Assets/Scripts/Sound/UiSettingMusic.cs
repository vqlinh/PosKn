using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSettingMusic : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;


    private void Start()
    {
        LoadVolumeSettings();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
        SaveVolumeSettings(Const.PrefsKey_MusicVolume, musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SfxVolume(sfxSlider.value);
        SaveVolumeSettings(Const.PrefsKey_SfxVolume, sfxSlider.value);
    }

    private void SaveVolumeSettings(string prefsKey, float value)
    {
        PlayerPrefs.SetFloat(prefsKey, value);
        PlayerPrefs.Save();
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey(Const.PrefsKey_MusicVolume))
        {
            float musicVolume = PlayerPrefs.GetFloat(Const.PrefsKey_MusicVolume);
            musicSlider.value = musicVolume;
            AudioManager.Instance.MusicVolume(musicVolume);
        }

        if (PlayerPrefs.HasKey(Const.PrefsKey_SfxVolume))
        {
            float sfxVolume = PlayerPrefs.GetFloat(Const.PrefsKey_SfxVolume);
            sfxSlider.value = sfxVolume;
            AudioManager.Instance.SfxVolume(sfxVolume);
        }
    }
}
