using UnityEngine;

public class TurretBuildController : MonoBehaviour
{
    private TurretPool turretPool;
    private TurretValidator turretValidator;
    private GridSnapper gridSnapper;
    private TurretBuildGuide turretBuildGuide;

    [Header("메인 카메라")]
    [SerializeField] private Camera mainCam;

    //추후 인스펙터 노출 X
    [SerializeField] private bool _isBuild = false;
    public bool isBuild => _isBuild;

#if UNITY_EDITOR
    [ContextMenu("Auto Assign")]
    private void AutoAssign()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();  
    }
#endif

    private void Awake()
    {
        turretPool = GetComponent<TurretPool>();
        turretValidator = GetComponent<TurretValidator>();
        gridSnapper = GetComponent<GridSnapper>();
        turretBuildGuide = GetComponent<TurretBuildGuide>();
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
        _isBuild = !_isBuild;
        turretBuildGuide.SetBuildGuideSprite(_isBuild);
    }
    
    public void BuildOn()
    {
        _isBuild = true;
    }

    public void BuildOff()
    {
        _isBuild = false;
    }

    //터렛 설치
    private void TryBuild()
    {
        if(!_isBuild) return;

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
