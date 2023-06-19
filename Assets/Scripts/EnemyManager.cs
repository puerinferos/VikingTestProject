using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyBrain enemyPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private int startEnemiesCount;
    [SerializeField] private int startEnemiesHP;
    
    [SerializeField] private float radiusRespawn;
    [SerializeField] private float respawnTime;
    
    private List<EnemyBrain> currentEnemies = new List<EnemyBrain>();

    public void OnStartGame()
    {
        StartCoroutine(StartGameWithDelay());
    }
    
    private IEnumerator StartGameWithDelay()
    {
        yield return new WaitForSeconds(1);
        if (currentEnemies.Count > 0)
        {
            currentEnemies.ForEach(x => Destroy(x.gameObject));
            currentEnemies.Clear();
        }
        Initialize(); 
    }
    private void Initialize()
    {
        for (int i = 0; i < startEnemiesCount; i++)
        {
            var randomCirclePos = Random.insideUnitCircle * radiusRespawn;
            var spawnPosition = player.position + new Vector3(randomCirclePos.x, 0, randomCirclePos.y);
            var enemyClone = Instantiate(enemyPrefab, spawnPosition, quaternion.identity);
            enemyClone.Initialise(player,startEnemiesHP);
            
            currentEnemies.Add(enemyClone);
        }

        EnemyBrain.OnDead += OnEnemyDeadHandler;
    }

    private void OnEnemyDeadHandler(EnemyBrain deadEnemy)
    {
        GameData.Score++;
        StartCoroutine(RespawnCoroutine(deadEnemy));
    }

    IEnumerator RespawnCoroutine(EnemyBrain deadEnemy)
    {
        yield return new WaitForSeconds(2.5f);
        
        deadEnemy.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(respawnTime);
        var randomCirclePos = Random.insideUnitCircle * radiusRespawn;
        var spawnPosition = player.position + new Vector3(randomCirclePos.x, 0, randomCirclePos.y);
        deadEnemy.maxHp++;
        deadEnemy.gameObject.SetActive(true);
        deadEnemy.Reset();
        deadEnemy.transform.position = spawnPosition;
    }
}