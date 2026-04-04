using UnityEngine;

[CreateAssetMenu(fileName = "TurretBeamData", menuName = "Scriptable Objects/TurretBeamData")]
public class TurretBeamData : ScriptableObject
{
    public int cost = 50;
    public int attackDamage = 15;
    
    public float attackRange = 3.5f;
    public float cooldown = 1f;
    public float shootingSpeed = 400f;
}
