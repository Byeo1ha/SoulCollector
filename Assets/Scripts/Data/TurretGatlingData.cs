using UnityEngine;

[CreateAssetMenu(fileName = "TurretGatlingData", menuName = "Scriptable Objects/TurretGatlingData")]
public class TurretGatlingData : ScriptableObject
{
    public int cost = 110;
    public int attackDamage = 7;
    
    public float attackRange = 3f;
    public float cooldown = 0.2f;
    public float shootingSpeed = 3f;
}
