using TMPro;
using UnityEngine;

public class SoulUI : MonoBehaviour
{
    [SerializeField] private TMP_Text soulText;

    public void UpdateSoulText(int amount)
    {
        soulText.text = amount.ToString();
    }
}