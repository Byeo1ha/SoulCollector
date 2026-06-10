using System.Collections.Generic;
using UnityEngine;

public class SorcererHitEffectPool : MonoBehaviour
{
    [Header("피격 이펙트")]
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private int maxEffects = 10;

    private readonly List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < maxEffects; i++)
        {
            GameObject obj = Instantiate(hitEffectPrefab, transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetEffect()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
            {
                return pool[i];
            }
        }

        return null;
    }
}
