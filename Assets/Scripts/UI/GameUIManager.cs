using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameClearPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private BlackBorder pauseBorder;
    [SerializeField] private SFXClick sfxClick;
    [SerializeField] private GameOverAnim gameOverAnim;
    [SerializeField] private GameClearAnim gameClearAnim;
    [SerializeField] private BlackBorder blackBorder;
    [SerializeField] private TurretBuildController turretBuildController;

    private bool isPaused;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false);
        //pausePanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        if (turretBuildController.isBuild) turretBuildController.BuildToggle();

        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        gameOverAnim.StartGameOverAnim();
        blackBorder.HalfFadeOut();
    }

    public void ShowGameClear()
    {

        if (turretBuildController.isBuild) turretBuildController.BuildToggle();

        Time.timeScale = 0f;
        gameClearPanel.SetActive(true);
        gameClearAnim.StartGameClearAnim();
        blackBorder.HalfFadeOut();
    }

    public void TogglePause()
    {
        PausePanelAnim pausePanelAnim = pausePanel.GetComponent<PausePanelAnim>();

        switch (isPaused)
        {
            case false:
                pausePanelAnim.PlayMoveToTargetPos();
                pauseBorder.HalfFadeOut();
                break;
            case true:
                pausePanelAnim.PlayMoveToOriginalPos();
                pauseBorder.HalfFadeIn();
                break;
        }

        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0f : 1f;

        //pausePanel.SetActive(isPaused);
        sfxClick.PlaySoundClick();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        //pausePanel.SetActive(false);
    }
}