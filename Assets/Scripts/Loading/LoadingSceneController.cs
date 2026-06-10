using System.Collections;
using TMPro;
using UnityEngine;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text loadingText;

    private void Start()
    {
        SceneLoadManager.Instance.LoadTargetScene();
        StartCoroutine(StartLoadingText());
    }

    private void OnDisable()
    {
        StopCoroutine(StartLoadingText());
    }

    private IEnumerator StartLoadingText()
    {
        while (true)
        {
            loadingText.text = "Loading.";
            yield return new WaitForSeconds(0.4f);
            loadingText.text = "Loading..";
            yield return new WaitForSeconds(0.4f);
            loadingText.text = "Loading...";
            yield return new WaitForSeconds(0.4f);
            loadingText.text = "Loading....";
            yield return new WaitForSeconds(0.4f);
        }
    }

    private IEnumerator CFadeIn(float startAlpha, float endAlpha)
    {
        loadingText.color = new Color(1f, 1f, 1f, startAlpha);
        float alpha = startAlpha;
        while (alpha > endAlpha)
        {
            alpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            loadingText.color = new Color(1f, 1f, 1f, alpha);
        }
    }   
}