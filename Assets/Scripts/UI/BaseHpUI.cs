using UnityEngine;
using UnityEngine.UI;

public class BaseHpUI : MonoBehaviour
{
    [SerializeField] private Image hpFillImage;

    public void UpdateHp(float currentHp, float maxHp)
    {
        float ratio = currentHp / maxHp;
        ratio = Mathf.Clamp01(ratio);

        hpFillImage.fillAmount = ratio;
    }
}