using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource bgmSource;

    private Coroutine _fadeOutCoroutine;
    private float _defaultBgmVolume;

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
            return;
        }

        bgmSource = GetComponent<AudioSource>();
        _defaultBgmVolume = bgmSource.volume;
    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;

        if (_fadeOutCoroutine != null)
        {
            StopCoroutine(_fadeOutCoroutine);
            _fadeOutCoroutine = null;
        }

        bgmSource.volume = _defaultBgmVolume;
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM(float fadeDuration = 1f)
    {
        if (_fadeOutCoroutine != null)
        {
            StopCoroutine(_fadeOutCoroutine);
        }

        _fadeOutCoroutine = StartCoroutine(CStopBGM(fadeDuration));
    }

    private IEnumerator CStopBGM(float fadeDuration)
    {
        float startVolume = bgmSource.volume;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            bgmSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            yield return null;
        }

        bgmSource.Stop();
        bgmSource.clip = null;
        bgmSource.volume = _defaultBgmVolume;

        _fadeOutCoroutine = null;
    }
}
