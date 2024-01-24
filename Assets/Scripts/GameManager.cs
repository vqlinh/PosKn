using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI txtMaxHeal;
    public TextMeshProUGUI txtCurrentHeal;

    public static GameManager instance;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void LoadSceneWait()
    {
        SceneManager.LoadScene("SceneWait");
    }
}
