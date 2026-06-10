using UnityEngine;
using UnityEngine.EventSystems;

public class TurretBuildController : MonoBehaviour
{
    private TurretPool turretPool;
    private TurretValidator turretValidator;
    private GridSnapper gridSnapper;
    private TurretBuildGuide turretBuildGuide;

    [SerializeField] private SFXClick sfxClick;
    [SerializeField] private CurrencyManager currencyManager;

    [Header("설치 이펙트")]
    [SerializeField] private ParticlePool beamBuildEffectPool;
    [SerializeField] private ParticlePool gatlingBuildEffectPool;
    [SerializeField] private ParticlePool sniperBuildEffectPool;

    [Header("메인 카메라")]
    [SerializeField] private Camera mainCam;

    //추후 인스펙터 노출 X
    [SerializeField] private bool _isBuild = false;
    public bool isBuild => _isBuild;

    [SerializeField] private TurretType turretType;
    public TurretType currentTurretType => turretType;

    private bool _isLeftClicked;

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
        InputManager.Instance.onLeftClick += OnLeftClick;
        InputManager.Instance.debugKey += BuildToggle;
    }

    private void OnDisable()
    {
        InputManager.Instance.onLeftClick -= OnLeftClick;
        InputManager.Instance.debugKey -= BuildToggle;
    }

    private void Update()
    {
        if (!_isLeftClicked) return;

        _isLeftClicked = false;
        TryBuild();
    }

    private void OnLeftClick()
    {
        _isLeftClicked = true;
    }

    public void BuildToggle()
    {
        _isBuild = !_isBuild;
        turretBuildGuide.SetBuildGuideSprite(_isBuild);
    }

    public void SetTurretTypeBeam()
    {
        turretType = TurretType.Beam;
    }

    public void SetTurretTypeGatling()
    {
        turretType = TurretType.Gatling;
    }

    public void SetTurretTypeSniper()
    {
        turretType = TurretType.Sniper;
    }

    //터렛 설치
    private void TryBuild()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!_isBuild) return;

        Vector3 mousePos = mainCam.ScreenToWorldPoint(InputManager.Instance.mouseVec);
        mousePos.z = 0;

        Vector2 point = gridSnapper.GetSnappedPosition(mousePos);

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
        
        GameObject turret = null;

        switch (turretType)
        {
            case TurretType.Beam:
                turret = turretPool.GetBeamTurret();
                break;
            case TurretType.Gatling:
                turret = turretPool.GetGatlingTurret();
                break;
            case TurretType.Sniper:
                turret = turretPool.GetSniperTurret();
                break;
            default:
                turret = null;
                return;
        }

        if (turret == null)
        {
            Debug.Log("Not Enough Turrets");
            return;
        }

        TurretBase turretBase = turret.GetComponent<TurretBase>();

        if (turretBase == null)
        {
            Debug.Log("TurretBase Missing");
            return;
        }

        if (currencyManager == null)
        {
            Debug.Log("CurrencyManager Missing");
            return;
        }

        if (!currencyManager.TrySpendSoul(turretBase.Cost))
        {
            Debug.Log("Not Enough Soul");
            return;
        }

        Vector3 buildPosition = gridSnapper.GetSnappedPosition(mousePos);

        turret.transform.position = buildPosition;
        turret.SetActive(true);
        PlayBuildEffect(turretType, buildPosition);
        sfxClick.PlaySoundClick();
    }

    private void PlayBuildEffect(TurretType type, Vector3 position)
    {
        ParticlePool particlePool = null;

        switch (type)
        {
            case TurretType.Beam:
                particlePool = beamBuildEffectPool;
                break;
            case TurretType.Gatling:
                particlePool = gatlingBuildEffectPool;
                break;
            case TurretType.Sniper:
                particlePool = sniperBuildEffectPool;
                break;
        }

        if (particlePool == null) return;

        GameObject particle = particlePool.GetPool();

        if (particle == null) return;

        particle.transform.position = position;
        particle.SetActive(true);
    }
}
