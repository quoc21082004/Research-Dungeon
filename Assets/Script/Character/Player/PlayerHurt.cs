using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerHurt : Damagable
{
    Player player;
    bool isHealthRegen, isManaRegen;
    public GameObject gameOverprefab;
    [SerializeField] UnityEvent OnStartCombat;
    [SerializeField] UnityEvent OnEndCombat;
    
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    #region Take Damage & Dead
    public override void TakeDamage(float amount, bool isCrit)
    {
        OnStartCombat?.Invoke();
        var _valueDef = isCrit ? Random.Range(0, player.defense * 0.5f) : player.defense;
        var _finalDmg = (int)Mathf.Max(0, amount - Mathf.Max(0, _valueDef));
        _finalDmg = Mathf.Max(0, (int)(_finalDmg / player.playerdata.otherStats.damageReduction));
        player.health = Mathf.Clamp(player.health - _finalDmg, 0, player.maxhealth);
        DamagePopManager.instance.CreateDamagePop(false, _finalDmg, new Vector3(transform.position.x, transform.position.y + 0.75f, 0f), transform);
        AssetManager.instance.assetData.SpawnBloodSfx(transform);
        Dead();
    }
    public override float CaculateDMG(float amt) { return -1; }
    private void Dead()
    {
        if (player.health <= 0)
        {
            OnEndCombat?.Invoke();
            gameObject.SetActive(false);
            gameOverprefab.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
    #endregion
    #region Heal Regen - Mana Regen
    public void RegenRecover()
    {
        if ((player.health < player.maxhealth) && !isHealthRegen) // health < max health
            StartCoroutine(hpRegen());
        if ((player.mana < player.maxmana) && !isManaRegen) // mana < max mana
            StartCoroutine(mpRegen());
    }
    IEnumerator hpRegen()
    {
        isHealthRegen = true;
        player.health = Mathf.Clamp(player.health + PartyController.player.playerdata.basicStats.health, 0, player.health);
        AssetManager.instance.assetData.SpawnRecoverEffect(ConsumableType.HealthPotion, transform.position, PartyController.player.transform);
        yield return new WaitForSeconds(2.5f);
        isHealthRegen = false;
    }
    IEnumerator mpRegen()
    {
        isManaRegen = true;
        player.mana = Mathf.Clamp(player.mana + PartyController.player.playerdata.basicStats.manaRegen, 0, player.maxmana);
        AssetManager.instance.assetData.SpawnRecoverEffect(ConsumableType.ManaPotion, transform.position, PartyController.player.transform);
        yield return new WaitForSeconds(2.5f);
        isManaRegen = false;
    }
    #endregion

    #region Recover From Potion
    public void PlayerRecoverHP(float hpPots)
    {
        player.health = Mathf.Clamp(player.health + hpPots, 0, player.maxhealth);
        DamagePopManager.instance.CreateRecoverPop(ConsumableType.HealthPotion, hpPots, new Vector2(transform.position.x, transform.position.y + 0.75f), PartyController.player.transform);
    }
    public void PlayerRecoverMP(float mpPots)
    {
        player.mana = Mathf.Clamp(player.mana + mpPots, 0, player.maxmana);
        DamagePopManager.instance.CreateRecoverPop(ConsumableType.ManaPotion, mpPots, new Vector2(transform.position.x, transform.position.y + 0.75f), PartyController.player.transform);
    }
    #endregion
}
