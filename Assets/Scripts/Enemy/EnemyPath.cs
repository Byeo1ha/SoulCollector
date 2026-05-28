using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public Transform[] GetWaypoints()
    {
        Transform[] waypoints = new Transform[transform.childCount];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }

        return waypoints;
    }
}