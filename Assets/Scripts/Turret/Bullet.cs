using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _shootingSpeed;
    [SerializeField] private float _damage;

    private void FixedUpdate()
    {
        if (_target == null || !_target.gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, _target.position, _shootingSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(_damage);
            }

            Debug.Log("적과 충돌");
            gameObject.SetActive(false);
        }
    }

    public void SetTarget(Transform target, float shootingSpeed)
    {
        if (target == null) return;

        _target = target;
        _shootingSpeed = shootingSpeed;
    }

    public void SetTarget(Transform target, float shootingSpeed, float damage)
    {
        SetTarget(target, shootingSpeed);
        _damage = damage;
    }
}
