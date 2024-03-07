using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSettingMusic : MonoBehaviour {
  public Slider musicSlider;
  public Slider sfxSlider;
  [SerializeField] private GameObject muteMusic;
  [SerializeField] private GameObject muteSfx;

  private void Start() {
    LoadVolumeSettings();
  }

  public void MusicVolume() {
    if (musicSlider.value == 0) {
      muteMusic.SetActive(true);
    } else {
      muteMusic.SetActive(false);
    }
    AudioManager.Instance.MusicVolume(musicSlider.value);
    SaveVolumeSettings(Constants.PrefsKey_MusicVolume, musicSlider.value);
  }

  public void SFXVolume() {
    if(sfxSlider.value == 0) {
      muteSfx.SetActive(true);
    } else {
      muteSfx.SetActive(false);
    }
    AudioManager.Instance.SfxVolume(sfxSlider.value);
    SaveVolumeSettings(Constants.PrefsKey_SfxVolume, sfxSlider.value);
  }

  private void SaveVolumeSettings(string prefsKey, float value) {
    PlayerPrefs.SetFloat(prefsKey, value);
    PlayerPrefs.Save();
  }

  private void LoadVolumeSettings() {
    if (PlayerPrefs.HasKey(Constants.PrefsKey_MusicVolume)) {
      float musicVolume = PlayerPrefs.GetFloat(Constants.PrefsKey_MusicVolume);
      musicSlider.value = musicVolume;
      AudioManager.Instance.MusicVolume(musicVolume);
    }

    if (PlayerPrefs.HasKey(Constants.PrefsKey_SfxVolume)) {
      float sfxVolume = PlayerPrefs.GetFloat(Constants.PrefsKey_SfxVolume);
      sfxSlider.value = sfxVolume;
      AudioManager.Instance.SfxVolume(sfxVolume);
    }
  }
}
