using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    private Transform[] waypoints;
    private int currentIndex;

    public void SetPath(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
        currentIndex = 0;

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.Log("이동 경로가 비어 있습니다.");
            return;
        }

        transform.position = waypoints[0].position;
    }

    private void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Move();
    }

    private void Move()
{
    if (currentIndex >= waypoints.Length)
    {
        gameObject.SetActive(false);
        return;
    }

    if (waypoints[currentIndex] == null)
    {
        Debug.LogError($"{name}: Waypoint {currentIndex}가 비어 있습니다.");
        return;
    }

    transform.position = Vector3.MoveTowards(
        transform.position,
        waypoints[currentIndex].position,
        enemyData.moveSpeed * Time.deltaTime
    );

    if (Vector3.Distance(transform.position, waypoints[currentIndex].position) < 0.01f)
    {
        currentIndex++;
    }
}
}