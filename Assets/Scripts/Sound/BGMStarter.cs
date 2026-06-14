using UnityEngine;

public class BGMStarter : MonoBehaviour
{
    [SerializeField] private AudioClip bgm;

    private void Awake()
    {
        if (SoundManager.Instance == null) 
        {
            Debug.LogWarning("SoundManager이 없습니다. BootStrap 씬을 다녀오지 않으셨다면, 해당 오류는 의도된 오류입니다.");
            return;
        }

        SoundManager.Instance.PlayBGM(bgm);
    }
}
