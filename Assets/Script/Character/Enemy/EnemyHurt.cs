using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHurt : MonoBehaviour , IDamagable , IKnockBack
{
    private Enemy enemy;
    [SerializeField] float thurst;
    [SerializeField] private UnityEvent OnStartCombat;
    [SerializeField] private UnityEvent OnEndCombat;
    bool canUse = false;
    private float knockTime = 0.75f;
    public delegate void OnEnemyEvent();
    public OnEnemyEvent onEnemyEvent;

    #region Main Method
    private void OnEnable()
    {
        enemy = GetComponent<Enemy>();
    }

    #endregion

    #region Resurb Method
    private void SetMoodEnemy()
    {
        if (enemy.Health.currentValue > ((enemy.Health.maxValue * 75) / 100))          // > 75% hp
            enemy.mood = EnemyMood.Normal;
        else if (enemy.Health.currentValue < ((enemy.Health.maxValue * 50) / 100) && enemy.Health.currentValue > ((enemy.Health.maxValue * 30) / 100))    // 50% < x < 30%
            enemy.mood = EnemyMood.Medium;
        else if (enemy.Health.currentValue < ((enemy.Health.maxValue * 30) / 100) && enemy.Health.currentValue > ((enemy.Health.maxValue * 1) / 100))         // 30 < x < 1
            enemy.mood = EnemyMood.Advance;
        if (enemy.mood == EnemyMood.Medium && !canUse)
        {
            canUse = true;
            onEnemyEvent?.Invoke();
        }
    }
    public IEnumerator DieCoroutine()
    {
        if (enemy.Health.currentValue <= 0)
        {
            enemy.isDead = true;
            DropLootItem();
            OnEndCombat?.Invoke();
            enemy.mood = EnemyMood.End;
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }
    }
    private void DropLootItem()
    {
        RewardManager.instance.SpawnLoot(enemy.enemystat.Type, transform.position);
        PartyController.IncreaseCoin(enemy.enemystat.goldReward);
        //PartyController.inventoryG.IncreaseGold(enemy.enemystat.goldReward);
        GameManager.instance?.AddExperience(enemy.enemystat.expReward);
    }
    #endregion

    #region Interface
    public void TakeDamage(float amount, bool isCrit)
    {
        OnStartCombat?.Invoke();
        var _finalDmg = CaculateDMG(amount, isCrit);
        enemy.Health.Decrease(Mathf.FloorToInt(_finalDmg)); 
        enemy.myanim.SetTrigger("Hit");
        DamagePopManager.instance.CreateDamagePop(isCrit, _finalDmg, new Vector3(transform.position.x, transform.position.y + 0.75f, 0f), transform);
        KnockBack(PartyController.player.transform, thurst, transform);
        SetMoodEnemy();
        StartCoroutine(DieCoroutine());
    }
    public float CaculateDMG(float amount, bool isCrit)
    {
        var _valueDef = isCrit ? Random.Range(0, enemy.defense * 0.5f) : enemy.defense;
        var _finalDmg = (int)Mathf.Max(0, amount - Mathf.Max(0, _valueDef));
        return Mathf.CeilToInt(_finalDmg);
    }
    public void KnockBack(Transform damageSouce, float thurstKnock, Transform parent)
    {
        Vector2 dir = (transform.position - damageSouce.position).normalized * thurstKnock * enemy.myrigid.mass;
        enemy.myrigid.AddForce(dir, ForceMode2D.Impulse);
        PoolManager.instance.Release(AssetManager.instance.assetData.knockbackEffect.gameObject, parent.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        StartCoroutine(CourtineKnock());
    }
    IEnumerator CourtineKnock()
    {
        yield return new WaitForSeconds(knockTime);
        enemy.myrigid.velocity = Vector2.zero;
    }
    #endregion
}
