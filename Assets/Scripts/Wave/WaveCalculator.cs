using UnityEngine;

public static class WaveCalculator
{
    public static int GetEnemyCount(int stage)
    {
        return 12 + Mathf.FloorToInt(Mathf.Pow(stage, 1.4f) * 1.5f);
    }

    public static float GetSpawnInterval(int stage)
    {
        return Mathf.Max(0.3f, 1.2f - stage * 0.03f);
    }

}
