using UnityEngine;

public class TurretBuildBtn : MonoBehaviour
{
    [SerializeField] private TurretBuildController turretBuildController;

    public void OnTurretBeamBtn()
    {
        SelectTurretType(TurretType.Beam);
    }

    public void OnTurretGatlingBtn()
    {
        SelectTurretType(TurretType.Gatling);
    }

    public void OnTurretSniperBtn()
    {
        SelectTurretType(TurretType.Sniper);
    }

    private void SelectTurretType(TurretType turretType)
    {
        if (turretBuildController.isBuild && turretBuildController.currentTurretType == turretType)
        {
            turretBuildController.BuildToggle();
            return;
        }

        switch (turretType)
        {
            case TurretType.Beam:
                turretBuildController.SetTurretTypeBeam();
                break;
            case TurretType.Gatling:
                turretBuildController.SetTurretTypeGatling();
                break;
            case TurretType.Sniper:
                turretBuildController.SetTurretTypeSniper();
                break;
        }

        if (!turretBuildController.isBuild)
        {
            turretBuildController.BuildToggle();
        }
    }
}
