using UnityEngine;

public class TurretBuildBtn : MonoBehaviour
{
    [SerializeField] private TurretBuildController turretBuildController;
    [SerializeField] private SFXClick sfxClick;

    public void OnTurretBeamBtn()
    {
        SelectTurretType(TurretType.Beam);
        sfxClick.PlaySoundClick();
    }

    public void OnTurretGatlingBtn()
    {
        SelectTurretType(TurretType.Gatling);
        sfxClick.PlaySoundClick();
    }

    public void OnTurretSniperBtn()
    {
        SelectTurretType(TurretType.Sniper);
        sfxClick.PlaySoundClick();
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
