using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHurt : MonoBehaviour , IDamagable
{
    PlayerCTL player;
    bool isHealthRegen, isManaRegen;
    public GameObject gameOverprefab;
    [SerializeField] UnityEvent OnStartCombat;
    [SerializeField] UnityEvent OnEndCombat;
    private void OnEnable()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCTL>();
    }

    #region Take Damage & Dead
    public void TakeDamage(float amount, bool isCrit)
    {
        OnStartCombat?.Invoke();
        var _finalDmg = CaculateDMG(amount, isCrit);
        player.Health.Decrease(Mathf.CeilToInt(_finalDmg));
        DamagePopManager.instance.CreateDamagePop(false, _finalDmg, new Vector3(transform.position.x, transform.position.y + 0.75f, 0f), transform);
        AssetManager.instance.assetData.SpawnBloodSfx(transform);
        Dead();
    }
    public float CaculateDMG(float amount, bool isCrit)
    {
        var _def = player.playerdata.basicStats.defense;
        var _critDef = isCrit ? Random.Range(0, _def * 0.5f) : _def;
        var _finalDmg = (int)Mathf.Max(0, amount - Mathf.Max(0, _critDef));
        _finalDmg = Mathf.Max(0, (int)(_finalDmg / player.playerdata.otherStats.damageReduction));
        return Mathf.CeilToInt(_finalDmg);
    }
    private void Dead()
    {
        if (player.Health.currentValue <= 0)
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
        if ((player.Health.currentValue < player.Health.maxValue) && !isHealthRegen) 
            StartCoroutine(hpRegen());
        if ((player.Mana.currentValue < player.Mana.maxValue) && !isManaRegen)
            StartCoroutine(mpRegen());
    }
    IEnumerator hpRegen()
    {
        var _healthRegen = player.playerdata.basicStats.healthRegen;
        isHealthRegen = true;
        player.Health.Increase((int)_healthRegen);
        AssetManager.instance.assetData.SpawnRecoverEffect(ConsumableType.HealthPotion, transform.position, PartyController.player.transform);
        yield return new WaitForSeconds(2.5f);
        isHealthRegen = false;
    }
    IEnumerator mpRegen()
    {
        var _manaRegen = player.playerdata.basicStats.manaRegen;
        isManaRegen = true;
        player.Mana.Increase(Mathf.CeilToInt(_manaRegen));
        AssetManager.instance.assetData.SpawnRecoverEffect(ConsumableType.ManaPotion, transform.position, PartyController.player.transform);
        yield return new WaitForSeconds(2.5f);
        isManaRegen = false;
    }
    #endregion

    #region Recover From Potion
    public void PlayerRecoverHP(float _hpPots)
    {
        player.Health.Increase(Mathf.CeilToInt(_hpPots));
        DamagePopManager.instance.CreateRecoverPop(ConsumableType.HealthPotion, _hpPots, new Vector2(transform.position.x, transform.position.y + 0.75f), PartyController.player.transform);
    }
    public void PlayerRecoverMP(float _mpPots)
    {
        player.Mana.Increase(Mathf.CeilToInt(_mpPots));
        DamagePopManager.instance.CreateRecoverPop(ConsumableType.ManaPotion, _mpPots, new Vector2(transform.position.x, transform.position.y + 0.75f), PartyController.player.transform);
    }
    #endregion
}
