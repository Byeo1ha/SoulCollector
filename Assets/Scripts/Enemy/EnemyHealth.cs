using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    private EnemyDie enemyDie;

    private float currentHp;

    private void OnEnable()
    {
        currentHp = enemyData.maxHp;
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0f)
            return;

        currentHp -= damage;

        if (currentHp <= 0f)
        {
            gameObject.SetActive(false); //<< 머지할 때 지울 것
            //enemyDie.Die(); << 머지할 때 되돌릴 것
        }
    }

    
}