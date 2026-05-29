using UnityEngine;

[CreateAssetMenu(fileName = "TurretGatlingData", menuName = "Scriptable Objects/TurretGatlingData")]
public class TurretGatlingData : TurretData
{
    private void Reset()
    {
        cost = 110;
        attackDamage = 7;

        attackRange = 3f;
        cooldown = 0.2f;
        shootingSpeed = 3f;
    }
}
