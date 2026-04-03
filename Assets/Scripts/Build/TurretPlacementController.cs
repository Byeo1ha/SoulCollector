using UnityEngine;

public class TurretPlacementController : MonoBehaviour
{
    [Header("메인 카메라")]
    [SerializeField] private Camera mainCam;

    [Header("터렛")]
    [SerializeField] private TurretPool turretPool;
    [SerializeField] private LayerMask buildZoneLayer;

    [Header("설치 좌표 보정")]
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private Vector2 gridOrign = Vector2.zero;
    
    //추후 인스펙터 노출 X
    [SerializeField] private bool isBuild = false;

#if UNITY_EDITOR
[ContextMenu("Auto Assign")]
private void AutoAssign()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        turretPool = GameObject.Find("Build Manager").GetComponent<TurretPool>();
        buildZoneLayer = LayerMask.GetMask("BuildZone");
    }
#endif

    private void Awake()
    {
        InputManager.instance.onLeftClick += TryBuild;
        InputManager.instance.debugKey += BuildOn;
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
        Debug.Log("좌클릭 수신 완료!");
        if(!isBuild) return;

        Vector3 mousePos = mainCam.ScreenToWorldPoint(InputManager.instance.mouseVec);
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

        turret.transform.position = GetSnappedPosition(mousePos);
        turret.SetActive(true);

        isBuild = false;
    }

    private bool CanBuildZone(Vector2 vec)
    {
        Collider2D hit = Physics2D.OverlapPoint(vec, buildZoneLayer);
        return hit != null;
    }

    private Vector3 GetSnappedPosition(Vector3 vec)
    {
        float x = Mathf.Floor((vec.x - gridOrign.x) / cellSize) * cellSize + gridOrign.x + cellSize * 0.5f;
        float y = Mathf.Floor((vec.y - gridOrign.y) / cellSize) * cellSize + gridOrign.y + cellSize * 0.5f;
        return new Vector3(x, y, 0f);
    }
}
