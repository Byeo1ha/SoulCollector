using System.Collections.Generic;
using UnityEngine;

public class LaserPool : MonoBehaviour
{
    [Header("레이저")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private int maxLasers = 10;

    private readonly List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < maxLasers; i++)
        {
            GameObject obj = Instantiate(laserPrefab, transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetLaser()
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
