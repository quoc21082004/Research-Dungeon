using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateTrigger : MonoBehaviour
{
    [SerializeField] GameObject enemyprefab;
    [SerializeField] Transform[] enemyPos;
    public List<GameObject> lists = new List<GameObject>();
    private bool isTrigger , isActive = false;
    public UnityEvent OnStageStart;
    public UnityEvent OnStageEnd;
    private int currentSpawner;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTrigger && collision.gameObject.CompareTag("Player")) 
        {
            TriggerStartEvent();
        }
    }
    private void TriggerStartEvent()
    {
        isTrigger = true;
        isActive = true;
        OnStageStart?.Invoke();
        foreach (var pos in enemyPos)
        {
            GameObject clone = PoolManager.instance.Release(enemyprefab, pos.position, Quaternion.identity);
            if (clone.gameObject.TryGetComponent<EnemySpawn>(out var enemy))
            {
                enemy.SpawnEnemy();
                lists.Add(clone);
            }
            currentSpawner++;
        }
    }
    private void Update()
    {
        for (int i = 0; i < lists.Count; i++)
        {
            bool isPause = false;
            if (currentSpawner <= 0)
            {
                isPause = true;
                return;
            }
            if (!lists[i].gameObject.activeSelf && !isPause)   
            {
                currentSpawner--;
                lists.RemoveAt(i);
            }
        }
        if (currentSpawner > 0 && !isActive)
            return;
        else if (currentSpawner <= 0 && isActive) 
        {
            isActive = false;
            OnStageEnd?.Invoke();
            lists.Clear();
        }
    }
}
