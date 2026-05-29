using UnityEngine;

public class TurretBuildGuide : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject buildGuideSprite;
    
    private TurretBuildController turretBuildController;
    private GridSnapper gridSnapper;

    private void Awake()
    {
        turretBuildController = GetComponent<TurretBuildController>();
        gridSnapper = GetComponent<GridSnapper>();
        buildGuideSprite.SetActive(false);
    }

    private void Update()
    {
        if(turretBuildController.isBuild)
        {
            Vector3 mousePos = mainCam.ScreenToWorldPoint(InputManager.Instance.mouseVec);
            mousePos.z = 0;
            Vector3 point = gridSnapper.GetSnappedPosition(mousePos);
            buildGuideSprite.transform.position = point;
        }
    }

    public void SetBuildGuideSprite(bool value)
    {
        buildGuideSprite.SetActive(value);
    }
}
