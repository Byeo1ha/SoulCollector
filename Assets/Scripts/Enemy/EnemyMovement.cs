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

        transform.position = Vector3.MoveTowards(
            transform.position,
            waypoints[currentIndex].position,
            enemyData.moveSpeed * speedMultiplier * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, waypoints[currentIndex].position) < 0.01f)
        {
            currentIndex++;
        }
    }
}