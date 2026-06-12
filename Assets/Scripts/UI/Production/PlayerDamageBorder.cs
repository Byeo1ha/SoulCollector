using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageBorder : MonoBehaviour
{
    [SerializeField] private Image hurtScreen;

    private Coroutine fadeCoroutine;

    private void Start()
    {
        hurtScreen.gameObject.SetActive(false);
    }

    public void StartDamage()
    {
        StartCoroutine(CStartDamage());
    }

    private IEnumerator CStartDamage()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        
        fadeCoroutine = StartCoroutine(CFadeOut(0f, 0.1f));

        yield return fadeCoroutine;

        fadeCoroutine = StartCoroutine(CFadeIn(0.1f, 0f));
    }

    private IEnumerator CFadeIn(float startAlpha, float endAlpha)
    {
        hurtScreen.color = new Color(1f, 0f, 0f, startAlpha);
        hurtScreen.gameObject.SetActive(true);
        float alpha = startAlpha;
        while (alpha > endAlpha)
        {
            alpha -= 0.02f;
            yield return new WaitForSecondsRealtime(0.01f);
            hurtScreen.color = new Color(1f, 0f, 0f, alpha);
        }
        hurtScreen.gameObject.SetActive(false);
        fadeCoroutine = null;
    }

    private IEnumerator CFadeOut(float startAlpha, float endAlpha)
    {
        hurtScreen.color = new Color(1f, 0f, 0f, startAlpha);
        hurtScreen.gameObject.SetActive(true);
        float alpha = startAlpha;
        while (alpha < endAlpha)
        {
            alpha += 0.02f;
            yield return new WaitForSecondsRealtime(0.01f);
            hurtScreen.color = new Color(1f, 0f, 0f, alpha);
        }
        fadeCoroutine = null;
    }
}
