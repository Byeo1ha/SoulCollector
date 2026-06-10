using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseHpUI : MonoBehaviour
{
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Image hpFillImage;

    public void UpdateHp(float currentHp, float maxHp)
    {
        hpText.text =
            $"{Mathf.CeilToInt(currentHp)} / {Mathf.CeilToInt(maxHp)}";

        hpFillImage.fillAmount =
            currentHp / maxHp;
    }
}