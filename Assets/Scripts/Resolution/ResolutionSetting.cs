using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionSetting : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private readonly Vector2Int[] resolutions =
    {
        new Vector2Int(1280, 720),
        new Vector2Int(1366, 768),
        new Vector2Int(1920, 1080),
        new Vector2Int(2560, 1440),
    };

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        resolutionDropdown.ClearOptions();

        resolutionDropdown.AddOptions(new List<string>
        {
            "1280 x 720 (HD)",
            "1366 x 768 (WXGA)",
            "1920 x 1080 (FHD)",
            "2560 x 1440 (QHD)",
        });

        int startIndex = GetCurrentResolutionIndex();

        resolutionDropdown.SetValueWithoutNotify(startIndex);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

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

        return 2; // 목록에 없으면 FHD
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