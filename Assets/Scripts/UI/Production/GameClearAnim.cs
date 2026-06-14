using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameClearAnim : MonoBehaviour
{
    [SerializeField] private Image gameClearPanel;
    [SerializeField] private Image gameClearBtn;
    [SerializeField] private TMP_Text gameClearText;

    public void StartGameClearAnim()
    {
        StartCoroutine(CStartGameClearAnim(0f, 1f));
    }

    private IEnumerator CStartGameClearAnim(float startAlpha, float endAlpha)
    {
        gameClearPanel.color = new Color(1f, 1f, 1f, startAlpha);
        gameClearBtn.color = new Color(1f, 1f, 1f, startAlpha);
        gameClearText.color = new Color(1f, 1f, 1f, startAlpha);
        float alpha = startAlpha;
        while (alpha < endAlpha)
        {
            alpha += 0.05f;
            yield return new WaitForSecondsRealtime(0.01f);
            gameClearPanel.color = new Color(1f, 1f, 1f, alpha);
            gameClearBtn.color = new Color(1f, 1f, 1f, alpha);
            gameClearText.color = new Color(1f, 1f, 1f, alpha);
        }
    }
}
