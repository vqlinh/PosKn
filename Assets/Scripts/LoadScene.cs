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
        SceneManager.LoadScene(Const.homeScene);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Const.mainMenu);
    }

    public void LoadSceneLevel1()
    {
        SceneManager.LoadScene(Const.level1);
    }

    public void LoadSceneLevel2()
    {
        SceneManager.LoadScene(Const.level2);
    }

    public void LoadSceneLevel3()
    {
        SceneManager.LoadScene(Const.level3);
    }

    public void LoadSceneLevel4()
    {
        SceneManager.LoadScene(Const.level4);
    }

    public void LoadSceneLevel5()
    {
        SceneManager.LoadScene(Const.level5);
    }
}
