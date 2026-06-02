using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    public int CurrentCrystal { get; private set; }

    [SerializeField] private int startCrystal = 0;

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
        CurrentCrystal = startCrystal;
        Debug.Log($"시작 크리스탈: {CurrentCrystal}");
    }

    public void AddCrystal(int amount)
    {
        if (amount <= 0)
            return;

        CurrentCrystal += amount;

        Debug.Log($"크리스탈 획득: +{amount} / 현재 크리스탈: {CurrentCrystal}");
    }

    public bool TrySpendCrystal(int amount)
    {
        if (amount <= 0)
            return false;

        if (CurrentCrystal < amount)
            return false;

        CurrentCrystal -= amount;

        Debug.Log($"크리스탈 사용: -{amount} / 현재 크리스탈: {CurrentCrystal}");

        return true;
    }
}