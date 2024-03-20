using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawns : MonoBehaviour
{

    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private bool canSpawn = true;

    private float _timeUntilSpawn;

    [SerializeField]
    private float _minimumSpawnTime = 1;
    [SerializeField]
    private float _maximumSpawnTime = 2;

    private bool spawnenemy = false;

    // Start is called before the first frame update
    void Start()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;

        if (_timeUntilSpawn <= 0)
        {
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            if (enemyToSpawn.gameObject.GetComponent<AIFollow>())
            {
                enemyToSpawn.gameObject.GetComponent<AIFollow>().EnemyType(rand, enemyToSpawn);
            }
            else
            {
                enemyToSpawn.gameObject.GetComponent<AIRangeFollow>().EnemyType(rand, enemyToSpawn);
            }
            SetTimeUntilSpawn();
        }        
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}
