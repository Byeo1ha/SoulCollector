using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleBtn : MonoBehaviour
{
    [System.Serializable]
    private class HoverButton
    {
        [SerializeField] private Button button;
        [SerializeField] private Image targetImage;
        [SerializeField] private Sprite hoverSprite;

        private Sprite originalSprite;

        public void SaveOriginalSprite()
        {
            if (targetImage != null)
                originalSprite = targetImage.sprite;
        }

        public void ChangeToHoverSprite(BaseEventData eventData)
        {
            if (targetImage == null || hoverSprite == null)
                return;

            targetImage.sprite = hoverSprite;
        }

        public void ChangeToOriginalSprite(BaseEventData eventData)
        {
            if (targetImage == null)
                return;

            targetImage.sprite = originalSprite;
        }

        public void AddPointerEvents()
        {
            if (button == null)
                return;

            EventTrigger eventTrigger = button.GetComponent<EventTrigger>();

            if (eventTrigger == null)
                eventTrigger = button.gameObject.AddComponent<EventTrigger>();

            AddEvent(eventTrigger, EventTriggerType.PointerEnter, ChangeToHoverSprite);
            AddEvent(eventTrigger, EventTriggerType.PointerExit, ChangeToOriginalSprite);
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

    private void Awake()
    {
        for (int i = 0; i < hoverButtons.Length; i++)
        {
            if (hoverButtons[i] == null)
                continue;

            hoverButtons[i].SaveOriginalSprite();
            hoverButtons[i].AddPointerEvents();
        }
    }

    public void GameStartBtn()
    {
        SceneLoadManager.Instance.LoadScene(nextSceneName);
    }

    public void GameQuitBtn()
    {
        Application.Quit();
    }
}
