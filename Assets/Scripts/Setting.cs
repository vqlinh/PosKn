using UnityEngine;

public class Setting : MonoBehaviour
{
    public GameObject pausePanel;

    public void Pause()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
