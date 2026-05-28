using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;

    private Transform[] waypoints;
    private int currentIndex;

    public void SetPath(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
        currentIndex = 0;

        if(waypoints == null || waypoints.Length == 0)
        {
            Debug.Log("이동 경로 비어있다");
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

        transform.position = Vector3.MoveTowards(
            transform.position,
            waypoints[currentIndex].position,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, waypoints[currentIndex].position) < 0.01f)
        {
            currentIndex++;
        }
    }
}
