using UnityEngine;

public class TurretValidator : MonoBehaviour
{
    [SerializeField] private LayerMask buildZoneLayer;
    [SerializeField] private LayerMask roadZoneLayer;
    [SerializeField] private LayerMask turretLayer;

#if UNITY_EDITOR
    [ContextMenu("Auto Assign")]
    private void AutoAssign()
    {
        buildZoneLayer = LayerMask.GetMask("BuildZone");
        roadZoneLayer = LayerMask.GetMask("RoadZone");
        turretLayer = LayerMask.GetMask("Turret");
    }
#endif

    public bool CanBuildZone(Vector2 vec)
    {
        Collider2D buildHit = Physics2D.OverlapPoint(vec, buildZoneLayer);
        if (buildHit == null)
            return false;

        Collider2D roadHit = Physics2D.OverlapPoint(vec, roadZoneLayer);
        return roadHit == null;
    }

    public bool HasTurret(Vector2 vec)
    {
        Collider2D hit = Physics2D.OverlapPoint(vec, turretLayer);
        return hit != null;
    }
}
