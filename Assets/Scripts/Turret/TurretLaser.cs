using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TurretSound))]
public class TurretLaser : TurretBase
{
    [SerializeField] private TurretData turretData;
    [SerializeField] private LaserPool laserPool;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private bool isFind = true;

    private ITurretAttackAnim _turretAttackAnim;
    private TurretSound turretSound;
    private TurretStat turretStat;
    private Transform _currentTarget;

    private float _originalScaleXValue;
    private float _nextFireTime = 0f;
    private bool _isAttackWaiting = false;

    public override int Cost => turretStat.cost;

    private void Awake()
    {
        _turretAttackAnim = GetComponentInChildren<ITurretAttackAnim>();
        turretSound = GetComponent<TurretSound>();
        _originalScaleXValue = transform.localScale.x;
        turretStat = turretData.RuntimeStat;
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

        LookAtTarget(target);

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
            target = FindFirstTarget(transform.position, turretStat.attackRange);

            if (target == null)
            {
                _isAttackWaiting = false;
                yield break;
            }

            LookAtTarget(target);
        }

        if (!isFind)
        {
            _isAttackWaiting = false;
            yield break;
        }

        turretSound.PlaySoundAttack();
        ShootLaser(target);

        _isAttackWaiting = false;
    }

    private void ShootLaser(Transform target)
    {
        GameObject laser = laserPool.GetLaser();

        if (laser == null) return;

        laser.transform.position = shootPoint.position;

        Vector2 direction = target.position - shootPoint.position;

        if (direction != Vector2.zero)
        {
            laser.transform.right = direction;
        }

        LaserProjectile laserProjectile = laser.GetComponent<LaserProjectile>();

        if (laserProjectile != null)
        {
            laserProjectile.SetDamage(turretStat.attackDamage);
        }

        laser.SetActive(true);
    }

    private void LookAtTarget(Transform target)
    {
        Vector3 currentPosition = transform.position;

        if (currentPosition.x < target.position.x)
        {
            transform.localScale = new Vector3(_originalScaleXValue, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(_originalScaleXValue * (-1), transform.localScale.y, transform.localScale.z);
        }
    }

    public void SetFind(bool value)
    {
        isFind = value;
    }   
}
