using UnityEngine;

public class TurretSound : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundAttack()
    {
        audioSource.Play();
    }
}
