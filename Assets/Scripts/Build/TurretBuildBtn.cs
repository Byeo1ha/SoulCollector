using UnityEngine;

public class TurretBuildBtn : MonoBehaviour
{
    [SerializeField] private TurretBuildController turretBuildController;

    public void OnTurretBeamBtn()
    {
        turretBuildController.SetTurretTypeBeam();
        BuildSwtiching();
    }

    public void OnTurretGatlingBtn()
    {
        turretBuildController.SetTurretTypeGatling();
        BuildSwtiching();
    }

    public void OnTurretSniperBtn()
    {
        turretBuildController.SetTurretTypeSniper();
        BuildSwtiching();
    }

    private void BuildSwtiching()
    {
        if (!turretBuildController.isBuild)
        turretBuildController.BuildToggle();
    }
}
