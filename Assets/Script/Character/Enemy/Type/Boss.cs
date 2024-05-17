using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public List<GameObject> monsterprefab;
    public Transform[] prefabPos;
    
    protected override void CheckDistance() { }

   /* private void Update()
    {
        if (!canUse && Health.currentValue <= (Health.maxValue / 30f))
        {
            canUse = true;
            BossSpawnEnemy();
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
   */
}
