using UnityEngine;

public class HitEffectDeActive : MonoBehaviour
{
    public void OnDeActive()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
