using System.Collections;
using TMPro;
using UnityEngine;

public class BootStrapManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "TitleScreen";
    [SerializeField] private GameObject targetObj;
    [SerializeField] private TMP_Text loadingText;

    private Coroutine LoadingText;

    private void Start()
    {
        StartCoroutine(StartBootStrap());
        LoadingText = StartCoroutine(StartLoadingText());
    }

    private IEnumerator StartBootStrap()
    {
        yield return new WaitForSeconds(0.5f);

        bool success = BootStrap();

        if (!success) 
        {
            Debug.Log("에디터 전용: BootStrap 검사에 실패했습니다. 해당 함수 확인 바람.");
            Application.Quit();
            yield break;
        }

        Debug.Log("부팅 성공");

        if (LoadingText != null)
        {
            StopCoroutine(LoadingText);
            LoadingText = null;
        }

        DeActiveLoadingObj();
        yield return new WaitForSeconds(0.2f);
        //페이드 아웃 연출은 어색해서 비동기 로드로 대체.
        //SceneManager.LoadScene(nextSceneName); << 완성되면 이 코드로 바꿀 것.
        SceneLoadManager.Instance.LoadSceneWithLoading(nextSceneName, 2f);
    }

    private bool BootStrap()
    {
        if (FadeManager.Instance == null) 
        {
            Debug.Log("FadeManager 누락 감지.");
            return false;
        }

        if (SceneLoadManager.Instance == null) 
        {
            Debug.Log("SceneLoadManager 누락 감지.");
            return false;
        }

        //성공적으로 마치셨어요!
        return true;
    }

// -------------------연출용------------------- //
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

    private void DeActiveLoadingObj()
    {
        targetObj.SetActive(false);
        loadingText.gameObject.SetActive(false);
    }
}
