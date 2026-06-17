using UnityEngine;

public class TitlePause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private PausePanelAnim pausePanelAnim;
    [SerializeField] private BlackBorder blackBorder;
    [SerializeField] private SFXClick sfxClick;

    public void PausePanelActive()
    {
        sfxClick.PlaySoundClick();
        blackBorder.HalfFadeOut();
        pausePanelAnim.PlayMoveToTargetPos();
    }

    public void PausePanelDeActive()
    {
        sfxClick.PlaySoundClick();
        blackBorder.HalfFadeIn();
        pausePanelAnim.PlayMoveToOriginalPos();
    }
}
