using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private float _spawnInterval;
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    [SerializeField] private List<GameObject> _enemyPrefabs = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            SpawnRandomEnemyAtRandomSpawn();
        }
    }
    private void SpawnRandomEnemyAtRandomSpawn()
    {
        GameObject randomEnemy = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];
        Transform randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        GameObject newEnemy = Instantiate(randomEnemy, randomSpawnPoint.position, Quaternion.identity);
        newEnemy.GetComponent<Rigidbody2D>().AddForce(-randomSpawnPoint.right * randomEnemy.GetComponent<EntityController>().EntityType.Speed, ForceMode2D.Impulse);
    }
}
