using UnityEngine;

public class DarkSorcererAnim : MonoBehaviour, ITurretAttackAnim
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
