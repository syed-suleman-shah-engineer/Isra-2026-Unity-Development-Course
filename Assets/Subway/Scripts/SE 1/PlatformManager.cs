using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject startingObject;

    public GameObject[] platformPrefabs; // Array of platform prefabs to spawn

    public float lineLength = 50f; // Length of the line along which platforms will be spawned

    public float spawnInterval = 2f; // Interval between platform spawns
    public float startingPlatformCount = 5f;


    private Transform _lastSpawnedPlatform; // Reference to the last spawned platform


    private float _spawnTimer; // Timer to track the time since the last platform spawn


    public void Start()
    {
        _lastSpawnedPlatform = startingObject.transform; // Set the last spawned platform to the starting object


        for (int i = 0; i < startingPlatformCount; i++)
        {
            SpawnPlatform(); // Spawn the initial platforms
        }

    }


    public void Update()
    {
        if ((Time.time - _spawnTimer) >= spawnInterval)
        {
            SpawnPlatform(); // Spawn a new platform
        }
    }


    private void SpawnPlatform()
    {
        // Choose a random platform prefab from the array
        GameObject platformPrefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];

        // Calculate the spawn position based on the last spawned platform's position and the line length
        Vector3 spawnPosition = _lastSpawnedPlatform.position + new Vector3(0f, 0f, lineLength);

        // Instantiate the platform prefab at the calculated spawn position
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        // Update the reference to the last spawned platform
        _lastSpawnedPlatform = newPlatform.transform;

        _spawnTimer = Time.time; // Reset the spawn timer
    }

}
