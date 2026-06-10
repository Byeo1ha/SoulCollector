using UnityEngine;

public class DropperSkill : MonoBehaviour
{
    [SerializeField] private int summonCount = 7;

    private EnemyPool ghoulPool;
    private EnemyMovement dropperMovement;
    private bool hasSummoned;

    private void Awake()
    {
        dropperMovement = GetComponent<EnemyMovement>();
    }

    private void OnEnable()
    {
        hasSummoned = false;
    }

    public void Initialize(EnemyPool pool)
    {
        ghoulPool = pool;
    }

    public void TrySummon(float currentHp, float maxHp)
    {
        if (hasSummoned)
            return;

        if (ghoulPool == null)
        {
            Debug.LogWarning("DropperSkill에 GhoulPool이 연결되지 않았습니다.");
            return;
        }

        if (currentHp > maxHp * 0.5f)
            return;

        hasSummoned = true;
        SummonGhouls();
    }

    private void SummonGhouls()
    {
        if (dropperMovement == null)
            return;

        Transform[] waypoints = dropperMovement.GetWaypoints();
        int startIndex = dropperMovement.GetCurrentIndex();

        for (int i = 0; i < summonCount; i++)
        {
            GameObject ghoul = ghoulPool.GetEnemy();

            if (ghoul == null)
                continue;

            ghoul.transform.position = transform.position;

            EnemyMovement ghoulMovement = ghoul.GetComponent<EnemyMovement>();

            if (ghoulMovement != null)
            {
                ghoulMovement.SetPathFromCurrentPosition(waypoints, startIndex);
            }

            EnemyDie enemyDie = ghoul.GetComponent<EnemyDie>();

            if (enemyDie != null)
            {
                enemyDie.SetRewardEnabled(false);
            }

            ghoul.SetActive(true);

            WaveManager.Instance.RegisterEnemy();
        }
    }
}