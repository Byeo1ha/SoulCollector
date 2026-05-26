using System.Collections.Generic;
using UnityEngine;

public class TurretPool : MonoBehaviour
{
    [Header("터렛")]
    [SerializeField] private GameObject beamTurretPrefab;
    [SerializeField] private GameObject gatlingTurretPrefab;
    [SerializeField] private GameObject sniperTurretPrefab;
    [SerializeField] private int maxTurrets = 10;
    
    private readonly List<GameObject> beamPool =  new List<GameObject>();
    private readonly List<GameObject> gatlingPool =  new List<GameObject>();
    private readonly List<GameObject> sniperPool =  new List<GameObject>();

    private void Awake()
    {
        CreatePool(beamTurretPrefab, beamPool);
        CreatePool(gatlingTurretPrefab, gatlingPool);
        CreatePool(sniperTurretPrefab, sniperPool);
    }

    public GameObject GetBeamTurret()
    {
        return GetPool(beamPool);
    }

    public GameObject GetGatlingTurret()
    {
        return GetPool(gatlingPool);
    }

    public GameObject GetSniperTurret()
    {
        return GetPool(sniperPool);
    }

    private void CreatePool(GameObject target, List<GameObject> pool)
    {
        for (int i = 0; i < maxTurrets; i++)
        {
            GameObject obj = Instantiate(target, transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    private GameObject GetPool(List<GameObject> pool)
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
