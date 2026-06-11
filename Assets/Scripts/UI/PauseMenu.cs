using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private void Start()
    {
        //pausePanel.SetActive(false);
    }

    public void OpenPause()
    {
        //pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        //pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}