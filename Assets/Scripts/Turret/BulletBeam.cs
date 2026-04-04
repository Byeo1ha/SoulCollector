using UnityEngine;

public class BulletBeam : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _shootingSpeed;

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _shootingSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
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
}
