using System.Collections;
using UnityEngine;

public class PausePanelAnim : MonoBehaviour
{
    [SerializeField] private RectTransform targetPos;
    [SerializeField] private RectTransform originalPos;
    [SerializeField] private float moveDuration = 0.3f;

    private RectTransform rectTransform;
    private Coroutine moveCoroutine;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = originalPos.anchoredPosition;
    }

    public void PlayMoveToTargetPos()
    {
        StartMove(targetPos);
    }

    public void PlayMoveToOriginalPos()
    {
        StartMove(originalPos);
    }

    private void StartMove(RectTransform destination)
    {
        if (destination == null)
            return;

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(CMovePanel(destination.anchoredPosition));
    }

    private IEnumerator CMovePanel(Vector2 destination)
    {
        float time = 0f;
        Vector2 startPos = rectTransform.anchoredPosition;

        while (time < moveDuration)
        {
            time += Time.unscaledDeltaTime;
            float t = time / moveDuration;
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, destination, t);
            yield return null;
        }

        rectTransform.anchoredPosition = destination;
        moveCoroutine = null;
    }
}
