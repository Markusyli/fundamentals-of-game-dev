using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public float spawnIdle = 4f;

    private PlayerManager playerManager;
    private Transform playerTransform;

    private Dictionary<int, Vector3> instancePositions = new Dictionary<int, Vector3>();

    private void Start()
    {
        playerManager = PlayerManager.instance;
        playerTransform = playerManager.player.transform;

        foreach(var spawnPoint in spawnPoints)
        {
            SpawnEnemy(spawnPoint.transform.position);
        }
    }

    private void OnEnable()
    {
        EnemyStats.OnEnemyKilled += TryReplaceEnemy;
    }

    private void TryReplaceEnemy(int instanceId)
    {
        if (instancePositions.ContainsKey(instanceId))
        {
            Vector3 spawnPosition = instancePositions[instanceId];
            instancePositions.Remove(instanceId);

            StartCoroutine(SpawnEnemyWithIdle(spawnPosition));
        }
    }

    private IEnumerator SpawnEnemyWithIdle(Vector3 position)
    {
        yield return new WaitForSeconds(spawnIdle);

        SpawnEnemy(position);
    }

    private void SpawnEnemy(Vector3 position)
    {
        enemyPrefab.GetComponent<CharacterAIControl>().target = playerTransform;
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        enemy.transform.LookAt(playerTransform.forward);

        instancePositions.Add(enemy.GetInstanceID(), position);
    }
}
