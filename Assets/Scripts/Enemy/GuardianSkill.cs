using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianSkill : MonoBehaviour
{
    [SerializeField] private float skillInterval = 5f;
    [SerializeField] private float stunDuration = 3f;
    [SerializeField] private int stunCount = 2;

    private Animator animator;
    private Coroutine skillCoroutine;

    private static readonly int SkillHash = Animator.StringToHash("Skill");

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        skillCoroutine = StartCoroutine(SkillRoutine());
    }

    private void OnDisable()
    {
        if (skillCoroutine != null)
        {
            StopCoroutine(skillCoroutine);
            skillCoroutine = null;
        }
    }

    private IEnumerator SkillRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(skillInterval);

            if (animator != null)
                animator.SetTrigger(SkillHash);

            StunRandomTurrets();
        }
    }

    private void StunRandomTurrets()
    {
        List<MonoBehaviour> turrets = new();

        turrets.AddRange(FindObjectsByType<TurretProjectile>(FindObjectsSortMode.None));
        turrets.AddRange(FindObjectsByType<TurretLaser>(FindObjectsSortMode.None));
        turrets.AddRange(FindObjectsByType<TurretArea>(FindObjectsSortMode.None));

        if (turrets.Count == 0)
            return;

        int count = Mathf.Min(stunCount, turrets.Count);

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, turrets.Count);
            MonoBehaviour selectedTurret = turrets[randomIndex];

            turrets.RemoveAt(randomIndex);

            StartCoroutine(StunRoutine(selectedTurret));
        }
    }

    private IEnumerator StunRoutine(MonoBehaviour turret)
    {
        if (turret == null)
            yield break;

        SetFind(turret, false);

        yield return new WaitForSeconds(stunDuration);

        if (turret != null)
        {
            SetFind(turret, true);
        }
    }

    private void SetFind(MonoBehaviour turret, bool value)
    {
        if (turret is TurretProjectile projectile)
        {
            projectile.SetFind(value);
        }
        else if (turret is TurretLaser laser)
        {
            laser.SetFind(value);
        }
        else if (turret is TurretArea area)
        {
            area.SetFind(value);
        }
    }
}