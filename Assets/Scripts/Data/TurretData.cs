using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "Scriptable Objects/TurretData")]
public class TurretData : ScriptableObject
{
    public int cost = 50;
    public int attackDamage = 15;

    public float attackRange = 5f;
    public float cooldown = 1f;
    public float attackDelay = 0f;
    public float shootingSpeed = 3f;

    private TurretStat runtimeStat;

    public TurretStat RuntimeStat
    {
        get
        {
            if (runtimeStat == null)
            {
                ResetRuntimeStat();
            }

            return runtimeStat;
        }
    }

    protected virtual void OnEnable()
    {
        ResetRuntimeStat();
    }

    public void ResetRuntimeStat()
    {
        runtimeStat = new TurretStat(cost, attackDamage, attackRange, cooldown, attackDelay, shootingSpeed);
    }
}
