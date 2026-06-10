using System.Collections.Generic;
using UnityEngine;

public abstract class TurretBase : MonoBehaviour
{
    [SerializeField] protected LayerMask enemyLayer;

    private readonly List<Transform> targetsInRange = new List<Transform>();

    public abstract int Cost { get; }

    protected abstract void TryAttack();

    protected virtual void OnDisable()
    {
        targetsInRange.Clear();
    }

    protected bool IsTargetValid(Transform target, Vector2 origin, float attackRanage)
    {
        if (target == null) return false;
        if (!target.gameObject.activeInHierarchy) return false;

        float distance = ((Vector2)target.position - origin).sqrMagnitude;
        return distance <= attackRanage * attackRanage;
    }

    protected virtual Transform FindFirstTarget(Vector2 origin, float attackRanage)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, attackRanage, enemyLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            Transform target = hits[i].transform;
            
            if (!targetsInRange.Contains(target))
            {
                targetsInRange.Add(target);
            }
        }

        for (int i = targetsInRange.Count - 1; i >= 0; i--)
        {
            if (!IsTargetValid(targetsInRange[i], origin, attackRanage))
            {
                targetsInRange.RemoveAt(i);
            }
        }

        if (targetsInRange.Count == 0) return null;

        return targetsInRange[0];
    }
}
