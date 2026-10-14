using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[Serializable]
public class GameView
{
    public TMP_Text scoreTmp;
    public TMP_Text timerTmp;

    public GameObject pauseMenu;
    public TMP_Text finalScoreTmp;
    public Button restart;


    public void Initialize()
    {
        scoreTmp.text = "00";
        pauseMenu.SetActive(false);
    }


    public void OnGameOver(int finalScore, UnityAction action)
    {
        finalScoreTmp.text = finalScore.ToString("00");

        restart.onClick.RemoveAllListeners();
        restart.onClick.AddListener(action);

        pauseMenu.SetActive(true);
    }
}

public class BucketGameManager : MonoBehaviour
{
    public static BucketGameManager Instance;

    private int Score;


    [Header("UI")]
    public GameView gameView;


    [Header("Spawnner")]
    public FallingObject fallingObjectPrefab;
    public Transform spawnerTrans;
    public Vector3 offsetPos;
    public float horizontalRange = 2f;
    public float spawnDelay = 2f;

    [Header("Timer")]
    public float startingTime = 60;


    public float fallingObjectDefaultSpeed = .5f;
    public float maxSpeedFallingObject = 1.75f;

    private float _lastSpawnAt;
    private float _startTime;

    private float _lastFoSpeed;


    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Start()
    {
        gameView.Initialize();
        _startTime = Time.time;
        _lastFoSpeed = fallingObjectDefaultSpeed;
    }


    public void Update()
    {
        if (Time.time - _lastSpawnAt > spawnDelay)
        {
            SpawnObject();
        }

        UpdateTime();
    }

    public void SpawnObject()
    {
        Vector3 spawnPos = spawnerTrans.position + offsetPos + new Vector3(UnityEngine.Random.Range(-horizontalRange, horizontalRange), 0, 0);

        var fo = Instantiate(fallingObjectPrefab, spawnPos, Quaternion.identity);
        fo.fallingSpeed = _lastFoSpeed;
        _lastSpawnAt = Time.time;
    }


    public void IncreaseScore()
    {
        Score++;
        gameView.scoreTmp.text = Score.ToString("00");
    }

    public void DecreaseScore()
    {
        Score--;
        gameView.scoreTmp.text = Score.ToString("00");
    }


    private void UpdateTime()
    {
        var estimateTime = Time.time - _startTime;

        var remainingTime = startingTime - estimateTime;

        gameView.timerTmp.text = remainingTime.ToString("00");

        if (remainingTime % 3 == 0)
        {
            _lastFoSpeed += .1f;
            _lastFoSpeed = Math.Clamp(_lastFoSpeed, .5f, maxSpeedFallingObject);
        }

        if (remainingTime <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0; // pause game

        gameView.OnGameOver(Score, () =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
        });
    }


}