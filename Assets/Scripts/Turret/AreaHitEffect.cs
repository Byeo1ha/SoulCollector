using System.Collections;
using UnityEngine;

public class AreaHitEffect : MonoBehaviour
{
    [SerializeField] private float activeTime = 0.2f;

    private void OnEnable()
    {
        StartCoroutine(DisableAfterDelay());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(activeTime);
        gameObject.SetActive(false);
    }
}
