using System;
using UnityEngine;
public class PartyController : Singleton<PartyController>
{
    public static PlayerCTL player;
    public static Inventory inventoryG;

    #region Main Method
    protected override void Awake()
    {
        base.Awake();

        if (player == null)
            player = transform.GetChild(0).gameObject.GetComponent<PlayerCTL>();

        if (inventoryG == null)
            inventoryG = new Inventory { Gold = player.playerdata.upgradeLevel.GetGold() };
    }
    private void OnApplicationQuit()
    {
        player.playerdata.upgradeLevel.SetGold(inventoryG.Gold);
    }
    #endregion

    #region Resurb Method
    public void Respawn(int lostRateExp,int lostRateGold)
    {
        player.isAlve = true;
        player.gameObject.SetActive(true);
        GameManager.instance.RespawnAfterDie(lostRateExp);
        inventoryG.IncreaseCoin(-(int)((lostRateGold * inventoryG.Gold) / 100));
    }
    public void FullRestore()
    {
        var _init = player.playerdata.basicStats;
        player.Health.InitValue(Mathf.CeilToInt(_init.GetHealth()), Mathf.CeilToInt(_init.GetHealth()));
        player.Mana.InitValue(Mathf.CeilToInt(_init.GetMana()), Mathf.CeilToInt(_init.GetMana()));
    }
    public static void AddExperience(float amount) => GameManager.instance.AddExperience(amount);
    #endregion
}
