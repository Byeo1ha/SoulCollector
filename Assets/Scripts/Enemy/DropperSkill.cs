using System.Collections;
using UnityEngine;

public class DropperSkill : MonoBehaviour
{
    [SerializeField] private int summonCount = 7;
    [SerializeField] private float summonInterval = 0.4f;

    private EnemyPool ghoulPool;
    private PlayerBase playerBase;
    private CurrencyManager currencyManager;
    private WaveManager waveManager;
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

    public void Initialize(
        EnemyPool pool,
        PlayerBase playerBase,
        CurrencyManager currencyManager,
        WaveManager waveManager)
    {
        ghoulPool = pool;
        this.playerBase = playerBase;
        this.currencyManager = currencyManager;
        this.waveManager = waveManager;
    }

    public void TrySummon(float currentHp, float maxHp)
    {
        if (hasSummoned)
            return;

        if (ghoulPool == null || waveManager == null)
        {
            Debug.LogWarning("DropperSkill 초기화가 완료되지 않았습니다.");
            return;
        }

        if (currentHp > maxHp * 0.5f)
            return;

        hasSummoned = true;
        StartCoroutine(SummonGhoulsRoutine());
    }

    private IEnumerator SummonGhoulsRoutine()
    {
        if (dropperMovement == null)
            yield break;

        Transform[] waypoints = dropperMovement.GetWaypoints();
        int startIndex = dropperMovement.GetCurrentIndex();

        for (int i = 0; i < summonCount; i++)
        {
            GameObject ghoul = ghoulPool.GetEnemy();

            if (ghoul != null)
            {
                ghoul.transform.position = transform.position;

                EnemyMovement ghoulMovement = ghoul.GetComponent<EnemyMovement>();

                if (ghoulMovement != null)
                {
                    ghoulMovement.SetPathFromCurrentPosition(waypoints, startIndex);
                }

                EnemyDie enemyDie = ghoul.GetComponent<EnemyDie>();

                if (enemyDie != null)
                {
                    enemyDie.Initialize(playerBase, currencyManager, waveManager);
                    enemyDie.SetRewardEnabled(false);
                }

                ghoul.SetActive(true);

                waveManager.RegisterEnemy();
            }

            yield return new WaitForSeconds(summonInterval);
        }
    }
}