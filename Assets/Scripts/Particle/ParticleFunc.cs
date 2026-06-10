using System.Collections;
using UnityEngine;

public class ParticleFunc : MonoBehaviour
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        if (particle == null) particle = GetComponent<ParticleSystem>();
        
        particle.Play();
        StartCoroutine(CDeActive());
    }

    private IEnumerator CDeActive()
    {
        yield return new WaitForSeconds(1.1f);
        gameObject.SetActive(false);
    }
}
