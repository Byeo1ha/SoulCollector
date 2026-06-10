using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private int maxParticle = 10;
    
    private readonly List<GameObject> pool =  new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < maxParticle; i++)
        {
            GameObject obj = Instantiate(particlePrefab, transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetPool()
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
