using UnityEngine;

public class TurretPlacementController : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private TurretPool turretPool;

    [SerializeField] private LayerMask buildZoneLayer;
    
    //추후 인스펙터 노출 X
    [SerializeField] private bool isBuild = false;

    private void Update()
    {
        if(!isBuild) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            TryBuild();
        }

        if (Input.GetMouseButtonDown(1))
        {
            
        }
    }
    
    public void BuildOn()
    {
        isBuild = true;
    }

    public void BuildOff()
    {
        isBuild = false;
    }

    private void TryBuild()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector2 point = mousePos;

        if (!CanBuildZone(point))
        {
            Debug.Log("Can't Build this Area");
            return;
        }
        
        GameObject turret = turretPool.GetTurret();

        if (turret == null)
        {
            Debug.Log("Not Enough Turrets");
            return;
        }

        turret.transform.position = mousePos;
        turret.SetActive(true);

        isBuild = false;
    }

    private bool CanBuildZone(Vector2 vec)
    {
        Collider2D hit = Physics2D.OverlapPoint(vec, buildZoneLayer);
        return hit != null;
    }
}
