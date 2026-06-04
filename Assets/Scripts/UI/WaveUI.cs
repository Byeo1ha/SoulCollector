using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text prepareTimeText;

    public void UpdateWaveText(int currentStage, int maxStage)
    {
        waveText.text = $"Wave {currentStage} / {maxStage}";
    }

    public void UpdatePrepareTime(float time)
    {
        prepareTimeText.text = $"정비 시간 : {Mathf.CeilToInt(time)}";
    }

    public void HidePrepareTime()
    {
        prepareTimeText.text = "";
    }
}