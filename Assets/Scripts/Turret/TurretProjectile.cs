using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : TurretBase
{
    [SerializeField] private TurretData turretData;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private int maxBullets = 10;

    [SerializeField] private bool isFind = true;

    private readonly List<GameObject> bulletPool = new List<GameObject>();

    private ITurretAttackAnim _turretAttackAnim;
    private TurretStat turretStat;
    private Transform _currentTarget;

    private float _originalScaleXValue;
    private float _nextFireTime = 0f;
    private bool _isAttackWaiting = false;

    private void Awake()
    {
        _turretAttackAnim = GetComponentInChildren<ITurretAttackAnim>();
        _originalScaleXValue = transform.localScale.x;
        turretStat = turretData.RuntimeStat;
        CreateBulletPool();
    }

    private void Update()
    {
        if (isFind) TryAttack();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopAllCoroutines();
        _isAttackWaiting = false;
    }

    private void CreateBulletPool()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    protected override void TryAttack()
    {
        _currentTarget = FindFirstTarget(transform.position, turretStat.attackRange);

        if (_currentTarget == null) return;

        Attack(_currentTarget);
    }

    private void Attack(Transform target)
    {
        if (Time.time < _nextFireTime) return;
        if (_isAttackWaiting) return;

        Vector3 currentPosition = transform.position;

        if (currentPosition.x < target.position.x) 
        transform.localScale = new Vector3(_originalScaleXValue, transform.localScale.y, transform.localScale.z);
        else transform.localScale = new Vector3(_originalScaleXValue * (-1), transform.localScale.y, transform.localScale.z);

        _nextFireTime = Time.time + turretStat.cooldown;

        _turretAttackAnim.OnAttackAnimation();
        StartCoroutine(AttackDelayCoroutine(target));
    }

    private IEnumerator AttackDelayCoroutine(Transform target)
    {
        _isAttackWaiting = true;

        if (turretStat.attackDelay > 0f)
        {
            yield return new WaitForSeconds(turretStat.attackDelay);
        }

        if (!IsTargetValid(target, transform.position, turretStat.attackRange))
        {
            _isAttackWaiting = false;
            yield break;
        }

        ShootBullet(target);
        _isAttackWaiting = false;
    }

    private void ShootBullet(Transform target)
    {
        GameObject bullet = GetBullet();

        if (bullet == null) return;

        bullet.transform.position = shootPoint.position;

        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        bulletComponent.SetTarget(target, turretStat.shootingSpeed, turretStat.attackDamage);

        bullet.SetActive(true);
    }

    private GameObject GetBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeSelf)
            {
                return bulletPool[i];
            }
        }

        return null;
    }
}
