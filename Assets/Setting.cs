using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public GameObject pausePanel;
    // Start is called before the first frame update
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
