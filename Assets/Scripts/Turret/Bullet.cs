using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private float _shootingSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private SorcererHitEffectPool hitEffectPool;

    private Vector3 _originalLocalScale;
    private Transform _poolParent;

    private void Awake()
    {
        _originalLocalScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        if (_target == null || !_target.gameObject.activeInHierarchy)
        {
            ReturnToPool();
            return;
        }

        UpdateScaleByMoveDirection();
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _shootingSpeed * Time.fixedDeltaTime);
    }

    private void OnDisable()
    {
        transform.localScale = _originalLocalScale;
        _target = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(_damage);
                PlayHitEffect(collision.transform.position);
            }

            Debug.Log("적과 충돌");
            ReturnToPool();
        }
    }

    public void SetTarget(Transform target, float shootingSpeed)
    {
        if (target == null) return;

        _target = target;
        _shootingSpeed = shootingSpeed;
        transform.localScale = _originalLocalScale;
        UpdateScaleByMoveDirection();
    }

    public void SetTarget(Transform target, float shootingSpeed, float damage)
    {
        SetTarget(target, shootingSpeed);
        _damage = damage;
    }

    public void SetPoolParent(Transform poolParent)
    {
        _poolParent = poolParent;
    }

    private void UpdateScaleByMoveDirection()
    {
        float directionX = _target.position.x - transform.position.x;

        if (Mathf.Approximately(directionX, 0f))
            return;

        float directionSign = directionX > 0f ? 1f : -1f;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(_originalLocalScale.x) * directionSign;
        transform.localScale = scale;
    }

    private void PlayHitEffect(Vector3 position)
    {
        if (hitEffectPool == null) return;

        GameObject hitEffect = hitEffectPool.GetEffect();

        if (hitEffect == null) return;

        hitEffect.transform.position = position;
        hitEffect.SetActive(true);
    }

    private void ReturnToPool()
    {
        if (_poolParent != null)
        {
            transform.SetParent(_poolParent, false);
        }

        transform.localScale = _originalLocalScale;
        _target = null;
        gameObject.SetActive(false);
    }
}
