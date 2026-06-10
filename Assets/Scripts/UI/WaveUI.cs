using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TMP_Text waveNumberText;
    [SerializeField] private TMP_Text statusSituationText;

    public void UpdateWaveText(int currentStage, int maxStage)
    {
        waveNumberText.text = $"{currentStage} / {maxStage}";
    }

    public void SetWaveState()
    {
        statusSituationText.text = "WAVE!";
    }

    public void SetBreakState()
    {
        statusSituationText.text = "BREAK";
    }
}