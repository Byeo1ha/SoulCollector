using UnityEngine;

public class BGMStarter : MonoBehaviour
{
    [SerializeField] private AudioClip bgm;

    private void Awake()
    {
        SoundManager.Instance.PlayBGM(bgm);
    }
}
