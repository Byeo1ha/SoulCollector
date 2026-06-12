using UnityEngine;

public class BootResolution : MonoBehaviour
{
    private readonly Vector2Int[] resolutions =
    {
        new Vector2Int(1280, 720),
        new Vector2Int(1366, 768),
        new Vector2Int(1920, 1080),
        new Vector2Int(2560, 1440),
    };
    
    private void Start()
    {
        int startIndex = GetCurrentResolutionIndex();

        SetResolution(startIndex);
    }

    private int GetCurrentResolutionIndex()
    {
        int currentWidth = Screen.currentResolution.width;
        int currentHeight = Screen.currentResolution.height;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].x == currentWidth &&
                resolutions[i].y == currentHeight)
            {
                return i;
            }
        }

        return 2;
    }

    private void SetResolution(int index)
    {
        Vector2Int resolution = resolutions[index];

        Screen.SetResolution(
            resolution.x,
            resolution.y,
            FullScreenMode.ExclusiveFullScreen
        );
    }
}
