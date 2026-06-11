using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private WaveUI waveUI;
    [SerializeField] private GameUIManager gameUIManager;

    [SerializeField] private float prepareTime = 30f;
    [SerializeField] private int maxStage = 20;

    [SerializeField] private AudioClip clearAudio;
    [SerializeField] private AudioClip failAudio;
    [SerializeField] private TurretBuildController turretBuildController;
    [SerializeField] private SFXClick sfxClick;

    public int CurrentStage { get; private set; } = 10;
    public bool IsGameEnded => isGameEnded;

    private AudioSource audioSource;
    private int aliveEnemyCount;
    private bool isSpawning;
    private Coroutine prepareCoroutine;
    private bool isGameEnded;
    private bool hasStartedFirstWave;

    private void PlaySound(string sound)
    {
        switch (sound)
        {
            case "Clear":
                audioSource.clip = clearAudio;
                break;
            case "Fail":
                audioSource.clip = failAudio;
                break;
        }
        audioSource.Play();
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartInitialPrepareTime();
    }

    public void RegisterEnemy()
    {
        aliveEnemyCount++;
    }

    public void UnregisterEnemy()
{
    Debug.Log(
    $"CurrentStage={CurrentStage}, Alive={aliveEnemyCount}, IsSpawning={isSpawning}"
    );
    aliveEnemyCount--;


    if (aliveEnemyCount > 0)
        return;

    if (isSpawning)
        return;

    if (CurrentStage >= maxStage)
    {
        Debug.Log("GameClear 진입");
        GameClear();
        return;
    }
    
    PlaySound("Clear");
    StartPrepareTime();
}

    public void SetSpawningState(bool value)
{
    isSpawning = value;

    if (isSpawning)
        return;

    if (aliveEnemyCount > 0)
        return;

    if (CurrentStage >= maxStage)
    {
        GameClear();
        return;
    }

    StartPrepareTime();
}

    public void EndGame()
    {
        isGameEnded = true;

        PlaySound("Fail");
        if (prepareCoroutine != null)
        {
            StopCoroutine(prepareCoroutine);
            prepareCoroutine = null;
        }
    }

    public void StartNextWaveImmediately()
    {
        if (prepareCoroutine == null)
            return;

        if (turretBuildController.isBuild)
            turretBuildController.BuildToggle();
        
        StopCoroutine(prepareCoroutine);
        prepareCoroutine = null;

        if (CurrentStage >= maxStage)
        {
            GameClear();
            return;
        }

        if (hasStartedFirstWave)
        {
            CurrentStage++;
        }

        sfxClick.PlaySoundClick();

        StartWave();
    }

    private void StartWave()
    {
        if (isGameEnded)
            return;

        hasStartedFirstWave = true;

        waveUI.UpdateWaveText(CurrentStage, maxStage);
        waveUI.SetWaveState();

        Debug.Log($"{CurrentStage} 웨이브 시작");
        StartCoroutine(waveSpawner.SpawnWave(CurrentStage));
    }

    private void StartPrepareTime()
    {
        if (isGameEnded)
            return;

        if (prepareCoroutine != null)
            return;

        prepareCoroutine = StartCoroutine(PrepareRoutine());
    }

    private void StartInitialPrepareTime()
    {
        waveUI.UpdateWaveText(CurrentStage, maxStage);
        waveUI.SetBreakState();

        prepareCoroutine = StartCoroutine(InitialPrepareRoutine());
    }

    private IEnumerator InitialPrepareRoutine()
    {
        Debug.Log("정비 시간 시작");

        yield return new WaitForSeconds(prepareTime);

        prepareCoroutine = null;
        StartWave();
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
        EndGame();

        if (gameUIManager != null)
        {
            gameUIManager.ShowGameClear();
        }
    }
}
