using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverAnim : MonoBehaviour
{
    [SerializeField] private Image gameOverPanel;
    [SerializeField] private Image gameOverBtn;
    [SerializeField] private TMP_Text gameOverText;

    public void StartGameOverAnim()
    {
        StartCoroutine(CStartGameOverAnim(0f, 1f));
    }

    private IEnumerator CStartGameOverAnim(float startAlpha, float endAlpha)
    {
        gameOverPanel.color = new Color(1f, 1f, 1f, startAlpha);
        gameOverBtn.color = new Color(1f, 1f, 1f, startAlpha);
        gameOverText.color = new Color(1f, 1f, 1f, startAlpha);
        float alpha = startAlpha;
        while (alpha < endAlpha)
        {
            alpha += 0.05f;
            yield return new WaitForSecondsRealtime(0.01f);
            gameOverPanel.color = new Color(1f, 1f, 1f, alpha);
            gameOverBtn.color = new Color(1f, 1f, 1f, alpha);
            gameOverText.color = new Color(1f, 1f, 1f, alpha);
        }
    }
}
