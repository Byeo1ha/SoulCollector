using System.Collections.Generic;
using UnityEngine;

public class TurretPool : MonoBehaviour
{
    [SerializeField] private GameObject turretPrefab;
    
    [SerializeField] private List<GameObject> pool =  new List<GameObject>();

    //오브젝트 풀링을 위한 Prefab 제작
    private void Awake()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            GameObject obj = Instantiate(turretPrefab, transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetTurret()
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
