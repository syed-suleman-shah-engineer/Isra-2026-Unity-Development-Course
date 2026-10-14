using UnityEngine;

public class BManager : MonoBehaviour
{
    public static BManager Instance;

    [Header("Spawning")]
    public Transform spawnPoint;
    public float spawnRangeHorizontal = 2f;

    public FObject fObjectPrefab;
    public Vector3 offsetPos;

    public float SpawnDelay = 2f; // spawn in 2 second interval

    [Header("Timer")]
    public float gameTimer = 60f;

    private float _onGameStartTime;

    public float timeLeft;


    private float _lastSpawnAt;


    [Header("Score")]
    public float score;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    void Start()
    {
        _onGameStartTime = Time.time;
    }


    public void Update()
    {
        if (Time.time - _lastSpawnAt > SpawnDelay)
        {
            SpawnFObject();
        }

        TimeUpdate();
    }


    public void SpawnFObject()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeHorizontal, spawnRangeHorizontal), 0, 0);

        spawnPos = spawnPoint.position + spawnPos + offsetPos;

        FObject obj = Instantiate(fObjectPrefab, spawnPos, Quaternion.identity);

        _lastSpawnAt = Time.time;
    }


    private void TimeUpdate()
    {
        var estimate = Time.time - _onGameStartTime;
        var remaining = gameTimer - estimate;

        timeLeft = remaining;

        if (remaining < 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0; // game over

        Debug.Log("Game Over");

        // UI pause menu popup
    }


    public void AddScore()
    {
        score++;
    }

    public void MinusScore()
    {
        score--;
    }



}