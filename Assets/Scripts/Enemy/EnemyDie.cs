using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public void Die()
    {
        gameObject.SetActive(false);
    }
}
