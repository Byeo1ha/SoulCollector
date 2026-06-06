using UnityEngine;

public class SoulKnightAnim : MonoBehaviour, ITurretAttackAnim
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnAttackAnimation()
    {
        anim.SetTrigger("isAttack");
    }
}
