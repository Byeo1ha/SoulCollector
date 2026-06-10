using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyDie : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private PlayerBase playerBase;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private WaveManager waveManager;

    [SerializeField] private float deathDelay = 0.8f;
    
    private AudioSource audioSource;
    private EnemyHealth enemyHealth;
    private EnemyMovement enemyMovement;
    private Animator animator;
    private SpriteRenderer[] spriteRenderers;
    private string originalTag;
    private int originalLayer;

    private bool isDead;
    private bool rewardEnabled = true;

    private const int EnemyDieLayer = 10;

    private static readonly int DieHash = Animator.StringToHash("Die");

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyMovement = GetComponent<EnemyMovement>();
        animator = GetComponentInChildren<Animator>();

        originalTag = gameObject.tag;
        originalLayer = gameObject.layer;

        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
    }

    private void OnEnable()
    {
        rewardEnabled = true;
        isDead = false;

        gameObject.tag = originalTag;
        gameObject.layer = originalLayer;

        ResetAnimator();
        ResetSprites();

        if (enemyMovement != null)
            enemyMovement.enabled = true;
    }

    public void SetRewardEnabled(bool value)
    {
        rewardEnabled = value;
    }

    public void Die()
    {
        if (isDead)
            return;

        isDead = true;

        audioSource.Play();
        gameObject.layer = EnemyDieLayer;

        GiveReward();

        if (enemyMovement != null)
            enemyMovement.enabled = false;

        StartCoroutine(DieRoutine());
    }

    public void ReachGoal()
    {
        if (isDead)
            return;

        isDead = true;

        if (playerBase != null)
        {
            playerBase.TakeDamage(enemyHealth.MaxHp);
        }

        RemoveEnemy();
    }

    private IEnumerator DieRoutine()
    {
        if (animator != null)
            animator.SetTrigger(DieHash);

        yield return new WaitForSeconds(deathDelay);

        if (animator != null)
        {
            animator.ResetTrigger(DieHash);
            animator.Play("Walk", 0, 0f);
            animator.Update(0f);
        }

        ResetSprites();

        RemoveEnemy();
    }

    private void ResetAnimator()
    {
        if (animator == null)
            return;

        animator.enabled = false;
        animator.enabled = true;

        animator.ResetTrigger(DieHash);
        animator.Play("Walk", 0, 0f);
        animator.Update(0f);
    }

    private void ResetSprites()
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            if (sr == null)
                continue;

            sr.enabled = true;
            sr.forceRenderingOff = false;

            Color color = sr.color;
            color.a = 1f;
            sr.color = color;
        }
    }

    private void GiveReward()       
    {
        if (!rewardEnabled)
            return;

        if (currencyManager == null)
            return;

        currencyManager.AddSoul(enemyData.crystalReward);
    }

    private void RemoveEnemy()
    {
        StopAllCoroutines();

        if (waveManager != null)
        {
            waveManager.UnregisterEnemy();
        }

        gameObject.SetActive(false);
    }

            public void Initialize(
            PlayerBase playerBase,
            CurrencyManager currencyManager,
            WaveManager waveManager)
        {
            this.playerBase = playerBase;
            this.currencyManager = currencyManager;
            this.waveManager = waveManager;
        }
}