using UnityEngine;

public class TurretBase : MonoBehaviour
{
    [SerializeField] protected LayerMask enemyLayer;

    protected bool IsTargetValid(Transform target, Vector2 origin, float attackRanage)
    {
        if (target == null) return false;
        if (!target.gameObject.activeInHierarchy) return false;

        float distance = ((Vector2)target.position - origin).sqrMagnitude;
        return distance <= attackRanage * attackRanage;
    }

    protected virtual Transform FindNearestTarget(Vector2 origin, float attackRanage)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, attackRanage, enemyLayer);

        if (hits.Length == 0) return null;

        Transform nearestTarget = null;
        float nearestDistance = float.MaxValue;

        for (int i = 0; i < hits.Length; i++)
        {
            float distance = ((Vector2)hits[i].transform.position - origin).sqrMagnitude;

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTarget = hits[i].transform;
            }
        }
        
        return nearestTarget;
    }
}
