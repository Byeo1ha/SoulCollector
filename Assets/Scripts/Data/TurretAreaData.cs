using UnityEngine;

[CreateAssetMenu(fileName = "TurretAreaData", menuName = "Scriptable Objects/TurretAreaData")]
public class TurretAreaData : TurretData
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
