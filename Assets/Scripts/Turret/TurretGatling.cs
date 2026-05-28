using UnityEngine;

public class TurretGatling : TurretBase
{
    [SerializeField] private TurretGatlingData turretGatlingData;
    [SerializeField] private BulletGatlingPool bulletGatlingPool;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private bool isFind;

    private Transform _currentTarget;
    private float _nextFireTime;

    private void Update()
    {
        if (isFind) TryAttack();
    }

    private void TryAttack()
    {
        _currentTarget = FindFirstTarget(transform.position, turretGatlingData.attackRange);

        if (_currentTarget == null) return;

        Attack(_currentTarget);
    }

    private void Attack(Transform target)
    {
        if(Time.time < _nextFireTime) return;

        _nextFireTime = Time.time + turretGatlingData.cooldown;
        
        GameObject bullet = bulletGatlingPool.GetBullet();

        if(bullet == null) return;

        bullet.transform.position = shootPoint.position;
        
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        bulletComponent.SetTarget(target, turretGatlingData.shootingSpeed);

        bullet.SetActive(true);
    }
}
