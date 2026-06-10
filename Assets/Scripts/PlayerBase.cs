using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private float maxHp = 1000f;
    [SerializeField] private BaseHpUI baseHpUI;

    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GameUIManager gameUIManager;

    public float CurrentHp { get; private set; }

    private void Awake()
    {
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
        if (waveManager != null)
        {
            waveManager.EndGame();
        }

        if (gameUIManager != null)
        {
            gameUIManager.ShowGameOver();
        }
    }
}