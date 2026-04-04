using System.Collections.Generic;
using UnityEngine;

public class BulletBeamPool : MonoBehaviour
{
    [Header("총알")]
    [SerializeField] private GameObject bulletBeamPrefab;
    [SerializeField] private int maxBullets = 10;
    
    private readonly List<GameObject> pool =  new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            GameObject obj = Instantiate(bulletBeamPrefab, transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetBullet()
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
