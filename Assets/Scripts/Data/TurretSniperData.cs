using UnityEngine;

[CreateAssetMenu(fileName = "TurretSniperData", menuName = "Scriptable Objects/TurretSniperData")]
public class TurretSniperData : ScriptableObject
{
    public int cost = 150;
    public int attackDamage = 100;
    
    public float attackRange = 10f;
    public float cooldown = 2.5f;
    public float shootingSpeed = 10f;
}
