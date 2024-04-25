using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : Enemy
{
    private EnemyHurt enemyHurt;
    public List<GameObject> monsterprefab;
    public Transform[] prefabPos;
    protected override void OnEnable()
    {
        base.OnEnable();
        enemyHurt = GetComponent<EnemyHurt>();
    }
    protected override void CheckDistance() { }

    private void Update()
    {
        if (!canUse)
        {
            canUse = true;
            enemyHurt.onEnemyEvent += BossSpawnEnemy;
        }
    }
    private void BossSpawnEnemy()
    {
        foreach (var pos in prefabPos)
        {
            if (PoolManager.instance.Release(monsterprefab[Random.Range(0, monsterprefab.Count)]).TryGetComponent<EnemySpawn>(out var enemySpawn))
            {
                enemySpawn.transform.position = pos.position;
                enemySpawn.SpawnEnemy();
            }
        }
    }
}
