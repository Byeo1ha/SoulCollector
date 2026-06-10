using UnityEngine;

[RequireComponent(typeof(EnemyDie))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    private EnemyDie enemyDie;
    private Transform[] waypoints;
    private int currentIndex;
    private float speedMultiplier = 1f;

    private void Awake()
    {
        enemyDie = GetComponent<EnemyDie>();
    }

    public void SetPath(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
        currentIndex = 0;

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("이동 경로가 비어 있습니다.");
            return;
        }

        transform.position = waypoints[0].position;
    }

    public void SetPathFromCurrentPosition(Transform[] newWaypoints, int startIndex)
    {
        waypoints = newWaypoints;
        currentIndex = startIndex;

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("이동 경로가 비어 있습니다.");
            return;
        }
    }

    public Transform[] GetWaypoints()
    {
        return waypoints;
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
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
        enemyDie.ReachGoal();
        return;
    }

    Transform targetWaypoint = waypoints[currentIndex];

    Vector3 direction = targetWaypoint.position - transform.position;

    if (Mathf.Abs(direction.x) > 0.01f)
    {
        Vector3 scale = transform.localScale;
        scale.x = direction.x > 0 ? -1f : 1f;
        transform.localScale = scale;
    }

    transform.position = Vector3.MoveTowards(
        transform.position,
        targetWaypoint.position,
        enemyData.moveSpeed * speedMultiplier * Time.deltaTime
    );

    if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.01f)
    {
        currentIndex++;
    }
}
}