using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
    [SerializeField] private AudioMixer bgmMixer;
    [SerializeField] private Slider bgmSlider;

    private void Start()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("bgmSound", 0);
    }

    public void BGMAudioControl()
    {
        float bgmSound = bgmSlider.value;
        PlayerPrefs.SetFloat("bgmSound", bgmSound);

        if (bgmSound == -40f)
            bgmMixer.SetFloat("BGM", -80);
        else
            bgmMixer.SetFloat("BGM", bgmSound);
    }
}
