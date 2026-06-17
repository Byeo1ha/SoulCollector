using UnityEngine;

public class TitleBtn : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "GamePlay";

    public void GameStartBtn()
    {
        SceneLoadManager.Instance.LoadScene(nextSceneName);
    }

    public void GameQuitBtn()
    {
        Application.Quit();
    }
}
