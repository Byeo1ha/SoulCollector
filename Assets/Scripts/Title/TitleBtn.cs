using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class TitleBtn : MonoBehaviour
{
    [System.Serializable]
    private class HoverButton
    {
        [SerializeField] private Button button;
        [SerializeField] private Image targetImage;
        [SerializeField] private Sprite hoverSprite;
        [SerializeField] private SFXClick sfxClick;

        private Sprite originalSprite;
        private Image fadeImage;
        private Coroutine fadeCoroutine;
        private float visibleAlpha = 1f;

        public void SaveOriginalSprite()
        {
            if (targetImage != null)
            {
                originalSprite = targetImage.sprite;
                visibleAlpha = targetImage.color.a;
                CreateFadeImage();
            }
        }

        public void ChangeToHoverSprite(MonoBehaviour coroutineRunner, float fadeDuration)
        {
            if (targetImage == null || hoverSprite == null)
                return;

            ChangeSprite(coroutineRunner, hoverSprite, fadeDuration);
        }

        public void ChangeToOriginalSprite(MonoBehaviour coroutineRunner, float fadeDuration)
        {
            if (targetImage == null)
                return;

            ChangeSprite(coroutineRunner, originalSprite, fadeDuration);
        }

        public void AddPointerEvents(MonoBehaviour coroutineRunner, float fadeDuration)
        {
            if (button == null)
                return;

            EventTrigger eventTrigger = button.GetComponent<EventTrigger>();

            if (eventTrigger == null)
                eventTrigger = button.gameObject.AddComponent<EventTrigger>();

            AddEvent(eventTrigger, EventTriggerType.PointerEnter, delegate { ChangeToHoverSprite(coroutineRunner, fadeDuration); });
            AddEvent(eventTrigger, EventTriggerType.PointerExit, delegate { ChangeToOriginalSprite(coroutineRunner, fadeDuration); });
        }

        private void ChangeSprite(MonoBehaviour coroutineRunner, Sprite nextSprite, float fadeDuration)
        {
            if (fadeImage == null)
                CreateFadeImage();

            if (fadeImage == null)
                return;

            if (fadeCoroutine != null)
                coroutineRunner.StopCoroutine(fadeCoroutine);

            if (targetImage.sprite == nextSprite)
            {
                fadeCoroutine = coroutineRunner.StartCoroutine(CrossFadeCurrentSprite(fadeDuration));
                return;
            }

            fadeCoroutine = coroutineRunner.StartCoroutine(CrossFadeSprite(nextSprite, fadeDuration));
        }

        private void CreateFadeImage()
        {
            if (targetImage == null || fadeImage != null)
                return;

            GameObject fadeObject = new GameObject(targetImage.name + " Fade Image");
            fadeObject.transform.SetParent(targetImage.transform, false);

            RectTransform rectTransform = fadeObject.AddComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.localScale = Vector3.one;

            fadeImage = fadeObject.AddComponent<Image>();
            fadeImage.raycastTarget = false;
            fadeImage.type = targetImage.type;
            fadeImage.preserveAspect = targetImage.preserveAspect;
            fadeImage.pixelsPerUnitMultiplier = targetImage.pixelsPerUnitMultiplier;
            fadeImage.sprite = targetImage.sprite;
            fadeImage.color = targetImage.color;
            SetAlpha(fadeImage, 0f);
        }

        private IEnumerator CrossFadeCurrentSprite(float fadeDuration)
        {
            yield return CrossFade(fadeDuration, targetImage.color.a, visibleAlpha, fadeImage.color.a, 0f);

            SetAlpha(targetImage, visibleAlpha);
            SetAlpha(fadeImage, 0f);

            fadeCoroutine = null;
        }

        private IEnumerator CrossFadeSprite(Sprite nextSprite, float fadeDuration)
        {
            fadeImage.sprite = nextSprite;

            yield return CrossFade(fadeDuration, targetImage.color.a, 0f, fadeImage.color.a, visibleAlpha);

            targetImage.sprite = nextSprite;
            SetAlpha(targetImage, visibleAlpha);
            SetAlpha(fadeImage, 0f);

            fadeCoroutine = null;
        }

        private IEnumerator CrossFade(float duration, float startTargetAlpha, float endTargetAlpha, float startFadeAlpha, float endFadeAlpha)
        {
            if (duration <= 0f)
            {
                SetAlpha(targetImage, endTargetAlpha);
                SetAlpha(fadeImage, endFadeAlpha);
                yield break;
            }

            float time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;

                float progress = time / duration;
                SetAlpha(targetImage, Mathf.Lerp(startTargetAlpha, endTargetAlpha, progress));
                SetAlpha(fadeImage, Mathf.Lerp(startFadeAlpha, endFadeAlpha, progress));

                yield return null;
            }

            SetAlpha(targetImage, endTargetAlpha);
            SetAlpha(fadeImage, endFadeAlpha);
        }

        private void SetAlpha(Image image, float alpha)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }

        private void AddEvent(EventTrigger eventTrigger, EventTriggerType eventType, UnityEngine.Events.UnityAction<BaseEventData> action)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventType;
            entry.callback.AddListener(action);
            eventTrigger.triggers.Add(entry);
        }
    }

    [SerializeField] private string nextSceneName = "GamePlay";
    [SerializeField] private HoverButton[] hoverButtons = new HoverButton[3];
    [SerializeField] private float fadeDuration = 0.15f;
    [SerializeField] private SFXClick sfxClick;

    private void Awake()
    {
        for (int i = 0; i < hoverButtons.Length; i++)
        {
            if (hoverButtons[i] == null)
                continue;

            hoverButtons[i].SaveOriginalSprite();
            hoverButtons[i].AddPointerEvents(this, fadeDuration);
        }
    }

    public void GameStartBtn()
    {
        sfxClick.PlaySoundClick();
        SceneLoadManager.Instance.LoadSceneWithLoading(nextSceneName, 2f);
    }

    public void GameQuitBtn()
    {
        sfxClick.PlaySoundClick();
        Application.Quit();
    }
}
