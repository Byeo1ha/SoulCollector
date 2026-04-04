using UnityEngine;

public class TurretBuildController : MonoBehaviour
{
    [Header("메인 카메라")]
    [SerializeField] private Camera mainCam;

    [Header("터렛")]
    [SerializeField] private TurretPool turretPool;
    [SerializeField] private TurretValidator turretValidator;
    [SerializeField] private GridSnapper gridSnapper;
    
    //추후 인스펙터 노출 X
    [SerializeField] private bool isBuild = false;

#if UNITY_EDITOR
    [ContextMenu("Auto Assign")]
    private void AutoAssign()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        turretPool = GetComponent<TurretPool>();
        turretValidator = GetComponent<TurretValidator>();
        gridSnapper = GetComponent<GridSnapper>();
    }
#endif

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        InputManager.instance.onLeftClick += TryBuild;
        InputManager.instance.debugKey += BuildToggle;
    }

    private void OnDisable()
    {
        InputManager.instance.onLeftClick -= TryBuild;
        InputManager.instance.debugKey -= BuildToggle;
    }

    public void BuildToggle()
    {
        isBuild = !isBuild;
    }
    
    public void BuildOn()
    {
        isBuild = true;
    }

    public void BuildOff()
    {
        isBuild = false;
    }

    //터렛 설치
    private void TryBuild()
    {
        if(!isBuild) return;

        Vector3 mousePos = mainCam.ScreenToWorldPoint(InputManager.instance.mouseVec);
        mousePos.z = 0;

        Vector2 point = mousePos;

        if (!turretValidator.CanBuildZone(point))
        {
            Debug.Log("Can't Build this Area");
            return;
        }

        if (turretValidator.HasTurret(point))
        {
            Debug.Log("Already Built");
            return;
        }
        
        GameObject turret = turretPool.GetTurret();

        if (turret == null)
        {
            Debug.Log("Not Enough Turrets");
            return;
        }

        turret.transform.position = gridSnapper.GetSnappedPosition(mousePos);
        turret.SetActive(true);
    }

    

    
}
