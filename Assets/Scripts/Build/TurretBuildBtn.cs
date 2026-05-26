using UnityEngine;

public class TurretBuildBtn : MonoBehaviour
{
    [SerializeField] private TurretBuildController turretBuildController;

    public void OnTurretBeamBtn()
    {
        turretBuildController.SetTurretTypeBeam();
        turretBuildController.BuildToggle();
    }

    public void OnTurretGatlingBtn()
    {
        turretBuildController.SetTurretTypeGatling();
        turretBuildController.BuildToggle();
    }

    public void OnTurretSniperBtn()
    {
        turretBuildController.SetTurretTypeSniper();
        turretBuildController.BuildToggle();
    }
}
