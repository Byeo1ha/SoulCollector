using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private WaveUI waveUI;
    [SerializeField] private float prepareTime = 30f;
    [SerializeField] private int maxStage = 20;

    public int CurrentStage { get; private set; } = 1;

    private int aliveEnemyCount;
    private bool isSpawning;
    private Coroutine prepareCoroutine;

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
        StartWave();
    }

    public void RegisterEnemy()
    {
        aliveEnemyCount++;
    }

    public void UnregisterEnemy()
    {
        aliveEnemyCount--;

        if (aliveEnemyCount <= 0 && !isSpawning)
        {
            StartPrepareTime();
        }
    }

    public void SetSpawningState(bool value)
    {
        isSpawning = value;

        if (!isSpawning && aliveEnemyCount <= 0)
        {
            StartPrepareTime();
        }
    }

    public void StartNextWaveImmediately()
{
    if (prepareCoroutine == null)
        return;

    StopCoroutine(prepareCoroutine);
    prepareCoroutine = null;

    if (CurrentStage >= maxStage)
    {
        GameClear();
        return;
    }

    CurrentStage++;
    StartWave();
}

    private void StartWave()
{
    waveUI.UpdateWaveText(CurrentStage, maxStage);
    waveUI.SetWaveState();

    StartCoroutine(waveSpawner.SpawnWave(CurrentStage));
}

    private void StartPrepareTime()
    {
        if (prepareCoroutine != null)
            return;

        prepareCoroutine = StartCoroutine(PrepareRoutine());
    }

    private IEnumerator PrepareRoutine()
{
    Debug.Log("정비 시간 시작");

    waveUI.SetBreakState();

    yield return new WaitForSeconds(prepareTime);

    prepareCoroutine = null;

    if (CurrentStage >= maxStage)
    {
        GameClear();
        yield break;
    }

    CurrentStage++;
    StartWave();
}

    private void GameClear()
    {
        Debug.Log("게임 클리어");
    }

    
}