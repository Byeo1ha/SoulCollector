public class TurretStat
{
    public int cost;
    public int attackDamage;

    public float attackRange;
    public float cooldown;
    public float shootingSpeed;

    public TurretStat(int cost, int attackDamage, float attackRange, float cooldown, float shootingSpeed)
    {
        this.cost = cost;
        this.attackDamage = attackDamage;
        this.attackRange = attackRange;
        this.cooldown = cooldown;
        this.shootingSpeed = shootingSpeed;
    }
}
