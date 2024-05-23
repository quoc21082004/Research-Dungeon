using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHurt : MonoBehaviour , IDamagable
{
    private PlayerCTL player;
    public GameObject gameOverprefab;

    [SerializeField] UnityEvent OnStartCombat;
    [SerializeField] UnityEvent OnEndCombat;
    private void Start() => player = PartyController.player;

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
        var _def = player.playerdata.basicStats.GetDef();
        var _critDef = isCrit ? Random.Range(0, _def * 0.5f) : _def;
        var _finalDmg = (int)Mathf.Max(0, amount - Mathf.Max(0, _critDef));
        _finalDmg = Mathf.Max(0, (int)(_finalDmg / player.playerdata.basicStats.GetDamageReduce()));
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
