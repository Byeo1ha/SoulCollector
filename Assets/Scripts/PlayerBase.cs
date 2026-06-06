using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public static PlayerBase Instance { get; private set; }

    [SerializeField] private float maxHp = 1000f;
    [SerializeField] private BaseHpUI baseHpUI;

    public float CurrentHp { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        CurrentHp = maxHp;
    }

    private void Start()
    {
        baseHpUI.UpdateHp(CurrentHp, maxHp);
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0f)
            return;

        CurrentHp -= damage;
        CurrentHp = Mathf.Max(CurrentHp, 0f);

        baseHpUI.UpdateHp(CurrentHp, maxHp);

        if (CurrentHp <= 0f)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("게임 오버");
    }
}