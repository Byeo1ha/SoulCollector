using UnityEngine;

public class SFXClick : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundClick()
    {
        audioSource.Play();
    }
}
