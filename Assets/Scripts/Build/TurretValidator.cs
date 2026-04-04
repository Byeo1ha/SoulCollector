using UnityEngine;

public class TurretValidator : MonoBehaviour
{
    [SerializeField] private LayerMask buildZoneLayer;
    [SerializeField] private LayerMask turretLayer;

#if UNITY_EDITOR
    [ContextMenu("Auto Assign")]
    private void AutoAssign()
    {
        buildZoneLayer = LayerMask.GetMask("BuildZone");
        turretLayer = LayerMask.GetMask("Turret");
    }
#endif

    public bool CanBuildZone(Vector2 vec)
    {
        Collider2D hit = Physics2D.OverlapPoint(vec, buildZoneLayer);
        return hit != null;
    }

    public bool HasTurret(Vector2 vec)
    {
        Collider2D hit = Physics2D.OverlapPoint(vec, turretLayer);
        return hit != null;
    }
}
