using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawns : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private bool canSpawn = true;
    private bool tutorial = true;
    private float _timeUntilSpawn;
    [SerializeField] private float _minimumSpawnTime = 1;
    [SerializeField] private float _maximumSpawnTime = 2;
    private Camera mainCamera;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        mainCamera = Camera.main;
        CalculateSpawnBounds();
        SetTimeUntilSpawn();
        OnDrawGizmosSelected();
    }

    void Update()
    {
        if (!tutorial && canSpawn)
        {
            _timeUntilSpawn -= Time.deltaTime;
            if (_timeUntilSpawn <= 0)
            {
                SpawnRandomEnemyOutsideCamera();
                SetTimeUntilSpawn();
            }
        }
    }

    void CalculateSpawnBounds()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

        // Calculate the camera's viewport bounds
        Vector3 lowerLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 upperRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Calculate the spawn area bounds outside of the camera view
        minX = lowerLeft.x - 7; // Adjust as needed
        maxX = upperRight.x + 7; // Adjust as needed
        minY = lowerLeft.y - 7; // Adjust as needed
        maxY = upperRight.y + 7; // Adjust as needed
    }

    void SpawnRandomEnemyOutsideCamera()
    {
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogError("No enemy prefabs assigned!");
            return;
        }

        // Generate random spawn position outside of the camera view
        Vector3 spawnPosition = new Vector3(
            Random.value > 0.5f ? Random.Range(minX - 4, minX) : Random.Range(maxX + 10, maxX),
            Random.value > 0.5f ? Random.Range(minY - 4, minY) : Random.Range(maxY + 10, maxY),
            0f
        );

        // Randomly select an enemy prefab
        GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Spawn the enemy at the calculated position
        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }

    public void SpawnPrefabs(bool spawn)
    {
        tutorial = spawn;
    }
    void OnDrawGizmosSelected()
    {
        // Draw a wireframe box representing the spawn area outside of the camera view
        Gizmos.color = Color.red;
        Vector3 min = new Vector3(minX, minY, 0f);
        Vector3 max = new Vector3(maxX, maxY, 0f);
        Gizmos.DrawWireCube((min + max) * 0.5f, max - min);
    }
}
