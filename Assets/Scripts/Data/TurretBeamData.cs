using UnityEngine;

[CreateAssetMenu(fileName = "TurretBeamData", menuName = "Scriptable Objects/TurretBeamData")]
public class TurretBeamData : TurretData
{
    private void Reset()
    {
        cost = 50;
        attackDamage = 15;

        attackRange = 5f;
        cooldown = 1f;
        shootingSpeed = 3f;
    }
}
