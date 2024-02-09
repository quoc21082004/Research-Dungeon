using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHurt : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] float thurst;
    [SerializeField] private UnityEvent OnStartCombat;
    [SerializeField] private UnityEvent OnEndCombat;
    bool canUse = false;
    public delegate void OnEnemyEvent();
    public OnEnemyEvent onEnemyEvent;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        //lootspawner = GetComponent<LootSpawner>();
    }
    private void SetMoodEnemy()
    {
        if (enemy.health > ((enemy.maxhealth * 75) / 100))          // > 75% hp
            enemy.mood = EnemyMood.Normal;
        else if (enemy.health < ((enemy.maxhealth * 50) / 100) && enemy.health > ((enemy.maxhealth * 30) / 100))    // 50% < x < 30%
            enemy.mood = EnemyMood.Medium;
        else if (enemy.health < ((enemy.maxhealth * 30) / 100) && enemy.health > ((enemy.maxhealth * 1) / 100))         // 30 < x < 1
            enemy.mood = EnemyMood.Advance;
    }
    public virtual void TakeDamage(float amount, bool isCrit)
    {
        OnStartCombat?.Invoke();
        amount = Mathf.Min(amount, amount - enemy.defense);
        enemy.health = Mathf.Clamp(enemy.health - amount, 0, enemy.maxhealth);
        enemy.myanim.SetTrigger("Hit");
        DamagePopManager.instance.CreateDamagePop(isCrit, amount, new Vector3(transform.position.x, transform.position.y + 0.75f, 0f), transform);
        enemy.GetKnockBack(PartyController.player.transform, thurst, transform);
        SetMoodEnemy();
        if (enemy.mood == EnemyMood.Medium && !canUse) 
        {
            canUse = true;
            onEnemyEvent?.Invoke();
        }
        StartCoroutine(Dead());
    }
    private void DropLootItem()
    {
        DropManager.instance.SpawnLoot(enemy.enemystat.Type, transform.position);
        PartyController.AddGold(enemy.enemystat.goldReward);
        GameManager.instance?.AddExperience(enemy.enemystat.expReward);
    }
    public IEnumerator Dead()
    {
        if (enemy.health <= 0)
        {
            enemy.isDead = true;
            DropLootItem();
            OnEndCombat?.Invoke();
            enemy.mood = EnemyMood.End;
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }
    }
}
