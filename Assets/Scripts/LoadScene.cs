using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadHomeScene()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
        SceneManager.LoadScene(Const.homeScene);
    }

    public void LoadMainMenu()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
        SceneManager.LoadScene(Const.mainMenu);
    }

    public void LoadSceneLevel1()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
        SceneManager.LoadScene(Const.level1);
    }

    public void LoadSceneLevel2()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
        SceneManager.LoadScene(Const.level2);
    }

    public void LoadSceneLevel3()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
        SceneManager.LoadScene(Const.level3);
    }

    public void LoadSceneLevel4()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
        SceneManager.LoadScene(Const.level4);
    }

    public void LoadSceneLevel5()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
        SceneManager.LoadScene(Const.level5);
    }
}
