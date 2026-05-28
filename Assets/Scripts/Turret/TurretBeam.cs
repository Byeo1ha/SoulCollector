using UnityEngine;

public class TurretBeam : TurretBase
{
    [SerializeField] private TurretBeamData turretBeamData;
    [SerializeField] private BulletBeamPool bulletBeamPool;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private bool isFind = false;

    private TurretStat turretStat;
    private Transform _currentTarget;
    private float _nextFireTime = 0f;

    private void Awake()
    {
        turretStat = turretBeamData.RuntimeStat;
    }

    private void Update()
    {
        if (isFind) TryAttack();
    }

    private void TryAttack()
    {
        _currentTarget = FindFirstTarget(transform.position, turretStat.attackRange);

        if (_currentTarget == null) return;

        Attack(_currentTarget);
    }

    private void Attack(Transform target)
    {
        if(Time.time < _nextFireTime) return;

        _nextFireTime = Time.time + turretStat.cooldown;
        
        GameObject bullet = bulletBeamPool.GetBullet();

        if(bullet == null) return;

        bullet.transform.position = shootPoint.position;
        
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        bulletComponent.SetTarget(target, turretStat.shootingSpeed);

        bullet.SetActive(true);
    }
}
