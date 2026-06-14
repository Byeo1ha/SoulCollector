using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager Instance { get; private set; }

    [SerializeField] private string loadingSceneName = "LoadingScene";

    private string _targetSceneName;
    private float _minLoadingTime;

    private bool loadingCall = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        if (FadeManager.Instance == null) return;
        
        StartCoroutine(CLoadScene(sceneName));
    }

    private IEnumerator CLoadScene(string sceneName)
    {
        FadeManager.Instance.FadeOut();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        yield return new WaitForSeconds(1.2f);

        if (loadingCall) 
        {
            yield return new WaitForSeconds(1.5f);
            loadingCall = false;
        }

        operation.allowSceneActivation = true;

        while (!operation.isDone) yield return null;

        FadeManager.Instance.FadeIn();
    }

    public void LoadSceneWithLoading(string sceneName, float minLoadingTime)
    {
        if (FadeManager.Instance == null) return;

        loadingCall = true;
        _targetSceneName = sceneName;
        _minLoadingTime = minLoadingTime;

        StartCoroutine(CLoadScene(loadingSceneName));
    }

    public void LoadTargetScene()
    {
        if (string.IsNullOrEmpty(_targetSceneName)) return;

        StartCoroutine(CLoadTargetScene());
    }

    private IEnumerator CLoadTargetScene()
    {
        yield return new WaitForSeconds(_minLoadingTime);

        yield return CLoadScene(_targetSceneName);

        _targetSceneName = null;
        _minLoadingTime = 0f; 
    }
}
