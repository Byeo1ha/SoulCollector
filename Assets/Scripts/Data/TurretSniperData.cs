using UnityEngine;

[CreateAssetMenu(fileName = "TurretSniperData", menuName = "Scriptable Objects/TurretSniperData")]
public class TurretSniperData : TurretData
{
    private void Reset()
    {
        cost = 150;
        attackDamage = 100;

        attackRange = 10f;
        cooldown = 2.5f;
        shootingSpeed = 10f;
    }
}
