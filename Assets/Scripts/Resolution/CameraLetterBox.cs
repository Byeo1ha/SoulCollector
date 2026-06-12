using UnityEngine;

public class CameraLetterBox : MonoBehaviour
{
    [SerializeField] private float targetAspect = 16f / 9f;

    private Camera cam;
    private int lastScreenWidth;
    private int lastScreenHeight;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        ApplyLetterbox();
    }

    private void Update()
    {
        if (Screen.width == lastScreenWidth && Screen.height == lastScreenHeight)
            ApplyLetterbox();
    }

    private void ApplyLetterbox()
    {
        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;

        float currentAspect = (float)Screen.width / Screen.height;
        float scaleHeight = currentAspect / targetAspect;

        Rect rect = cam.rect;

        if (scaleHeight < 1f)
        {
            rect.width = 1f;
            rect.height = scaleHeight;
            rect.x = 0f;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            float scaleWidth = 1f / scaleHeight;

            rect.width = scaleWidth;
            rect.height = 1f;
            rect.x = (1f - scaleWidth) / 2f;
            rect.y = 0f;
        }

        cam.rect = rect;
    }
}
