using UnityEngine;

public class PlatfomControl : MonoBehaviour
{
    public GameObject startingBase;
    public GameObject[] platformPrefabs;

    [Header("Settings")]
    public Vector3 spawnOffset = new Vector3(0, 0, 50);
    public float delay = 2f;
    public float platformOnStart = 3; // no of 3 platfrom should spawn on start..


    [Header("Platform Settings")]
    public float moveSpeed = 3f;
    public Vector3 destoryOnPosition = new Vector3(0, 0, -300);


    private Transform _lastPlatform;
    private float _lastSpawnTime;

    public void Start()
    {
        _lastPlatform = startingBase.transform;

        startingBase.GetComponent<PlatformMove>().Initialize(moveSpeed, destoryOnPosition);

        for (int i = 0; i < platformOnStart; i++)
        {
            SpawnPlatform();
        }
    }


    private void SpawnPlatform()
    {
        // get an random platform from the list..
        var platformPrefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];

        // spawn the object
        var platform = Instantiate(platformPrefab);

        platform.GetComponent<PlatformMove>().Initialize(moveSpeed, destoryOnPosition);

        platform.transform.position = _lastPlatform.position + spawnOffset;

        _lastPlatform = platform.transform;

        _lastSpawnTime = Time.time;
    }

    void Update()
    {
        if ((Time.time - _lastSpawnTime) > delay)
        {
            SpawnPlatform();
        }
    }



}
