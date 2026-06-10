using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyDie : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private float deathDelay = 0.8f;

    private EnemyHealth enemyHealth;
    private EnemyMovement enemyMovement;
    private Animator animator;
    private Collider2D[] colliders;
    private SpriteRenderer[] spriteRenderers;
    private string originalTag;
    private int originalLayer;

    private bool isDead;
    private bool rewardEnabled = true;


    private static readonly int DieHash = Animator.StringToHash("Die");

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyMovement = GetComponent<EnemyMovement>();
        animator = GetComponentInChildren<Animator > ();

        originalTag = gameObject.tag;
        originalLayer = gameObject.layer;

        colliders = GetComponentsInChildren<Collider2D>(true);
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
    SetColliders(true);

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

    GiveReward();

    if (enemyMovement != null)
        enemyMovement.enabled = false;

    SetColliders(false);

    gameObject.tag = "Untagged";
    gameObject.layer = LayerMask.NameToLayer("Default");

    StartCoroutine(DieRoutine());
}

    public void ReachGoal()
    {
        if (isDead)
            return;

        isDead = true;

        SetColliders(false);

        PlayerBase.Instance.TakeDamage(enemyHealth.MaxHp);
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

    private void SetColliders(bool value)
    {
        foreach (Collider2D col in colliders)
        {
            if (col == null)
                continue;

            col.enabled = value;
        }
    }

    private void GiveReward()
{
    if (!rewardEnabled)
        return;

    if (CurrencyManager.Instance == null)
        return;

    CurrencyManager.Instance.AddSoul(enemyData.crystalReward);
}

    private void RemoveEnemy()
    {
        StopAllCoroutines();

        WaveManager.Instance.UnregisterEnemy();
        gameObject.SetActive(false);
    }
}