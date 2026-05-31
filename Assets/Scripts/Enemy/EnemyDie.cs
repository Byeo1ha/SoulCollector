using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public void Die()
    {
        WaveManager.Instance.UnregisterEnemy();
        gameObject.SetActive(false);
    }
}