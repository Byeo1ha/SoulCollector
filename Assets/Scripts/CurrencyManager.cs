using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [SerializeField] private int startSoul = 0;
    [SerializeField] private SoulUI soulUI;

    public int CurrentSoul { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        CurrentSoul = startSoul;
        soulUI.UpdateSoulText(CurrentSoul);
    }

    public void AddSoul(int amount)
    {
        if (amount <= 0)
            return;

        CurrentSoul += amount;
        soulUI.UpdateSoulText(CurrentSoul);
    }

    public bool TrySpendSoul(int amount)
    {
        if (amount <= 0)
            return false;

        if (CurrentSoul < amount)
            return false;

        CurrentSoul -= amount;
        soulUI.UpdateSoulText(CurrentSoul);

        return true;
    }
}