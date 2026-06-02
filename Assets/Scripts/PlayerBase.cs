using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public static PlayerBase Instance { get; private set; }

    [SerializeField] private float maxHp = 1000f;

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

    public void TakeDamage(float damage)
    {
        if (damage <= 0f)
            return;

        CurrentHp -= damage;

        Debug.Log($"본진 피해: {damage} / 남은 체력: {CurrentHp}");

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