using UnityEngine;

public class TurretBuildGuide : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private SpriteRenderer buildGuideSprite;
    [SerializeField] private Color buildableColor = new Color(0f, 1f, 0f, 0.6f);
    [SerializeField] private Color blockedColor = new Color(1f, 0f, 0f, 0.6f);
    
    private TurretBuildController turretBuildController;
    private TurretValidator turretValidator;
    private GridSnapper gridSnapper;

    private void Awake()
    {
        turretBuildController = GetComponent<TurretBuildController>();
        turretValidator = GetComponent<TurretValidator>();
        gridSnapper = GetComponent<GridSnapper>();
        buildGuideSprite.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(turretBuildController.isBuild)
        {
            Vector3 mousePos = mainCam.ScreenToWorldPoint(InputManager.Instance.mouseVec);
            mousePos.z = 0;
            Vector3 point = gridSnapper.GetSnappedPosition(mousePos);
            buildGuideSprite.transform.position = point;
            UpdateBuildGuideColor(point);
        }
    }

    public void SetBuildGuideSprite(bool value)
    {
        buildGuideSprite.gameObject.SetActive(value);
    }

    private void UpdateBuildGuideColor(Vector2 point)
    {
        if (turretValidator.CanBuild(point))
            buildGuideSprite.color = buildableColor;
        else
            buildGuideSprite.color = blockedColor;
    }
}
