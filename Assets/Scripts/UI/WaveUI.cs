using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TMP_Text waveNumberText;
    [SerializeField] private TMP_Text statusSituationText;

    private readonly Color breakColor = new Color(
        0.7688679f,
        0.8621966f,
        1f,
        1f
    );

    private readonly Color waveColor = new Color(
        1f,
        0.4618971f,
        0.2216981f,
        1f
    );

    public void UpdateWaveText(int currentStage, int maxStage)
    {
        waveNumberText.text = $"{currentStage} / {maxStage}";
    }

    public void SetWaveState()
    {
        statusSituationText.text = "WAVE!";
        statusSituationText.color = waveColor;
    }

    public void SetBreakState()
    {
        statusSituationText.text = "BREAK";
        statusSituationText.color = breakColor;
    }
}