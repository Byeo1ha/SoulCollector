using UnityEngine;

public class GridSnapper : MonoBehaviour
{
    [Header("설치 좌표 보정")]
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private Vector2 gridOrign = Vector2.zero;

    public Vector3 GetSnappedPosition(Vector3 vec)
    {
        float x = Mathf.Floor((vec.x - gridOrign.x) / cellSize) * cellSize + gridOrign.x + cellSize * 0.5f;
        float y = Mathf.Floor((vec.y - gridOrign.y) / cellSize) * cellSize + gridOrign.y + cellSize * 0.5f;
        return new Vector3(x, y, 0f);
    }
}
