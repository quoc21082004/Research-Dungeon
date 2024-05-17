using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateTrigger : MonoBehaviour
{
    [SerializeField] GameObject enemyprefab;
    [SerializeField] Transform[] enemyPos;
    private Collider2D mycollider;
    public List<GameObject> lists = new();
    private bool isTrigger, isActive;
    public UnityEvent OnStageStart;
    public UnityEvent OnStageEnd;
    private int currentSpawner;

    [SerializeField] string startChallenge, endChallenge;
    #region Main Method
    private void Awake() => mycollider = GetComponent<Collider2D>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTrigger && collision.gameObject.CompareTag("Player"))
            TriggerStartEvent();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isTrigger && collision.gameObject.CompareTag("Player"))
            TriggerEndEvent();
    }
    private void Update()
    {
        if (!enemyprefab) return;
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
    #endregion
    public void TriggerStartEvent()
    { 
        isTrigger = true;
        isActive = true;
        OnStageStart?.Invoke();
        NoticeManager.instance.EnableCompleteChallenge(startChallenge);
        if (!enemyprefab)
            return;
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
    public void TriggerEndEvent()
    {
        mycollider.enabled = false;
        isTrigger = false;
        isActive = false;
        OnStageEnd?.Invoke();
        NoticeManager.instance.EnableCompleteChallenge(endChallenge);

    }
}
