using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHurt : Damagable
{
    private Enemy enemy;
    [SerializeField] float thurst;
    [SerializeField] private UnityEvent OnStartCombat;
    [SerializeField] private UnityEvent OnEndCombat;
    bool canUse = false;
    public delegate void OnEnemyEvent();
    public OnEnemyEvent onEnemyEvent;
    private void OnEnable()
    {
        enemy = GetComponent<Enemy>();
    }
    private void SetMoodEnemy()
    {
        if (enemy.health > ((enemy.maxhealth * 75) / 100))          // > 75% hp
            enemy.mood = EnemyMood.Normal;
        else if (enemy.health < ((enemy.maxhealth * 50) / 100) && enemy.health > ((enemy.maxhealth * 30) / 100))    // 50% < x < 30%
            enemy.mood = EnemyMood.Medium;
        else if (enemy.health < ((enemy.maxhealth * 30) / 100) && enemy.health > ((enemy.maxhealth * 1) / 100))         // 30 < x < 1
            enemy.mood = EnemyMood.Advance;
        if (enemy.mood == EnemyMood.Medium && !canUse)
        {
            canUse = true;
            onEnemyEvent?.Invoke();
        }
    }
    private void DropLootItem()
    {
        RewardManager.instance.SpawnLoot(enemy.enemystat.Type, transform.position);
        PartyController.IncreaseCoin(enemy.enemystat.goldReward);
        GameManager.instance?.AddExperience(enemy.enemystat.expReward);
    }
    public IEnumerator Die()
    {
        if (enemy.health <= 0)
        {
            enemy.isDead = true;
            DropLootItem();
            OnEndCombat?.Invoke();
            enemy.OnDieEvent?.Invoke();
            enemy.mood = EnemyMood.End;
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }
    }
    public override void TakeDamage(float amount, bool isCrit)
    {
        OnStartCombat?.Invoke();
        enemy.OnTakeDamageEvent?.Invoke();
        var _valueDef = isCrit ? Random.Range(0, enemy.defense * 0.5f) : enemy.defense;
        var _finalDmg = (int)Mathf.Max(0, amount - Mathf.Max(0, _valueDef));
        enemy.health = Mathf.Clamp(enemy.health - _finalDmg, 0, enemy.maxhealth);
        enemy.myanim.SetTrigger("Hit");
        DamagePopManager.instance.CreateDamagePop(isCrit, _finalDmg, new Vector3(transform.position.x, transform.position.y + 0.75f, 0f), transform);
        enemy.GetKnockBack(PartyController.player.transform, thurst, transform);
        SetMoodEnemy();
        StartCoroutine(Die());
    }
    public override float CaculateDMG(float amt)
    {
        var _enemyATK = enemy.damage;
        var _minEnemyATK = enemy.player.defense + Random.Range(10, _enemyATK / 2);
        var modifiedEnemyATK = Mathf.CeilToInt(_enemyATK * (amt / 100.0f));
        return Mathf.Max(_minEnemyATK, modifiedEnemyATK);
    }
    public void HandlePlayerDie()
    {
        //gameObject.SetActive(false);
    }
}
