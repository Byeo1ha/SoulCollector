using UnityEngine;

public class TurretBeam : TurretBase
{
    [SerializeField] private TurretBeamData turretBeamData;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private bool isFind = false;

    private float _nextFireTime = 0f;

    private void Update()
    {
        if (isFind) TryAttack();
    }

    private void TryAttack()
    {
        Transform target = FindNearestTarget(transform.position, turretBeamData.attackRange);

        if (target == null) return;

        Attack(target);
    }

    private void Attack(Transform target)
    {
        if(Time.time < _nextFireTime) return;

        _nextFireTime = Time.time + turretBeamData.cooldown;
        
        GameObject bullet = bulletPool.GetBullet();

        if(bullet == null) return;

        bullet.transform.position = shootPoint.position;
        
        BulletBeam bulletBeam = bullet.GetComponent<BulletBeam>();
        bulletBeam.SetTarget(target, turretBeamData.shootingSpeed);

        bullet.SetActive(true);
    }
}
