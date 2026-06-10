using UnityEngine;

public class EnemyDeActive : MonoBehaviour
{
    public void DeActive()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
