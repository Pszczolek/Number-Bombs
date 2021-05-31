using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public DifficultyOptions difficultyOptions;
    public static BombSpawner Instance;
    public List<Bomb> spawnedBombs;
    public float timeBetweenSpawns = 3f;
    public float bombSpeed;
    public int maxNumber = 100;
    public int minNumber = 1;
    public OperationType operationType;
    public bool isSpawning = true;

    public Action<Bomb> OnBombDestroyed;

    [SerializeField]
    Bomb bombPrefab;
    [SerializeField]
    Transform leftSpawnPoint;
    [SerializeField]
    Transform rightSpawnPoint;
    [SerializeField]
    GameSession gameSession;

    private Difficulty _difficulty;
    private int _difficultyIncrementIndex = 0;
    private Coroutine _spawnCoroutine;
    private Coroutine _difficultyIncrementCoroutine;

    private void Awake()
    {
        Instance = this;
        StartSpawn();
        gameSession.ResetGameSession(_difficulty.difficultyName);
    }

    private void Start()
    {
        GameManager.Instance.OnGameOver += GameOver;
        GameManager.Instance.OnGameRestart += Restart;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameOver -= GameOver;
        GameManager.Instance.OnGameRestart -= Restart;
    }

    public void SpawnBomb()
    {
        float randomX = UnityEngine.Random.Range(leftSpawnPoint.position.x, rightSpawnPoint.position.x);
        float randomZ = UnityEngine.Random.Range(leftSpawnPoint.position.z, rightSpawnPoint.position.z);
        Vector3 position = new Vector3(randomX, transform.position.y, randomZ);
        var newBomb = Instantiate(bombPrefab, position, Quaternion.identity, transform);
        spawnedBombs.Add(newBomb);
        GenerateOperation(newBomb);
        //newBomb.SetOperation(Random.Range(1, 10), Random.Range(1,10), OperationType.Multiplication);
        newBomb.SetMovement(new Vector3(0, -bombSpeed, 0));
    }

    public void SetDifficulty()
    {
        _difficulty = difficultyOptions.SelectedDifficulty;
        timeBetweenSpawns = 60 / _difficulty.bombsPerMinute;
        bombSpeed = _difficulty.bombSpeed;
        maxNumber = _difficulty.maxNumber;
        minNumber = _difficulty.minNumber;
        operationType = _difficulty.allowedOperations;
    }

    public void BombDestroyed(Bomb destroyedBomb)
    {
        spawnedBombs.Remove(destroyedBomb);
        if(OnBombDestroyed != null)
        {
            OnBombDestroyed(destroyedBomb);
        }
    }

    public void BombHit(Bomb hitBomb)
    {
        Player.Instance.AddScore(hitBomb.scoreValue);
        ParticleTextController.Instance.BombHit(hitBomb);
    }

    public void BombMissed(Bomb missedBomb)
    {
        gameSession.AddMissedEquation(missedBomb);
        Player.Instance.Hit();
        ParticleTextController.Instance.BombLost(missedBomb);
    }

    public void GameOver()
    {
        StopSpawn();
        foreach(var bomb in spawnedBombs)
        {
            bomb.SetMovement(Vector3.zero);
        }
    }

    public void Restart()
    {
        gameSession.ResetGameSession(_difficulty.difficultyName);
        ClearBombs();
        StartSpawn();
    }

    public void StartSpawn()
    {
        isSpawning = true;
        SetDifficulty();
        if(_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        if (_difficultyIncrementCoroutine != null)
        {
            StopCoroutine(_difficultyIncrementCoroutine);
        }
        _difficultyIncrementCoroutine = StartCoroutine(DifficultyCoroutine());
    }

    public void StopSpawn()
    {
        isSpawning = false;
    }

    public void ClearBombs()
    {
        foreach(var bomb in spawnedBombs)
        {
            Destroy(bomb.gameObject);
        }
        spawnedBombs.Clear();
    }

    private IEnumerator SpawnCoroutine()
    {
        while (isSpawning)
        {
            SpawnBomb();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        
    }

    private IEnumerator DifficultyCoroutine()
    {
        do
        {
            yield return new WaitForSeconds(_difficulty.timeToIncrement);
            IncrementDifficulty();
        }
        while (isSpawning);
    }

    private void IncrementDifficulty()
    {
        _difficultyIncrementIndex++;
        timeBetweenSpawns = 60 / (_difficulty.bombsPerMinute + _difficultyIncrementIndex * _difficulty.bombsPerMinuteIncrement);
        bombSpeed = _difficulty.bombSpeed + _difficultyIncrementIndex * _difficulty.bombSpeedIncrement;
    }
    private OperationType RandomizeOperation()
    {
        //bool[] x = new bool[4];
        List<int> availableOps = new List<int>();
        int opr = (int)operationType;
        int currentOpr = 1;
        if (opr == -1)
            opr = 15;
        for (int i = 0; i < 4; i++)
        {
            //Debug.Log($"Current: {currentOpr}");
            //Debug.Log($"Opr: {opr}");
            //x[i] = ((opr % 2) == 1);
            if ((opr % 2) == 1)
            {
                availableOps.Add(currentOpr);
            }
            opr /= 2;
            currentOpr *= 2;
            //Debug.Log(x[i]);

        }
        return (OperationType)availableOps[UnityEngine.Random.Range(0, availableOps.Count)];

    }

    private void GenerateOperation(Bomb bomb)
    {
        int numA = 0;
        int numB = 0;
        int sqrtMin = Mathf.FloorToInt(Mathf.Sqrt(minNumber));
        int sqrtMax = Mathf.FloorToInt(Mathf.Sqrt(maxNumber));
        OperationType randomOperation = RandomizeOperation();
        switch (randomOperation)
        {
            case OperationType.Summation:
                numA = UnityEngine.Random.Range(minNumber, maxNumber);
                numB = UnityEngine.Random.Range(minNumber, maxNumber - numA);
                break;
            case OperationType.Substraction:
                numA = UnityEngine.Random.Range(minNumber, maxNumber + 1);
                numB = UnityEngine.Random.Range(minNumber - 1, numA + 1);
                break;
            case OperationType.Multiplication:
                numA = UnityEngine.Random.Range(sqrtMin, sqrtMax);
                numB = UnityEngine.Random.Range(sqrtMin, sqrtMax);
                break;
            case OperationType.Division:
                numA = UnityEngine.Random.Range(sqrtMin, sqrtMax);
                numB = UnityEngine.Random.Range(sqrtMin, sqrtMax);
                numA *= numB;
                break;
        }
        bomb.SetOperation(numA, numB, randomOperation);
    }
}
