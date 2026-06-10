using System.Collections;
using UnityEngine;

public class BulletEffectDeActive : MonoBehaviour
{
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(CDeActive());
    }

    private void OnDisable()
    {
        if (_coroutine == null) return;

        StopCoroutine(_coroutine);
        _coroutine = null;

    }

    private IEnumerator CDeActive()
    {
        yield return new WaitForSeconds(0.45f);
        gameObject.SetActive(false);
    }
}
