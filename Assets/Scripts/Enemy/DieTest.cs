using UnityEngine;

public class DieTest : MonoBehaviour
{
    [SerializeField]private Animator anim;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("Die");
        }
    }
}
