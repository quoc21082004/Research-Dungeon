using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : Singleton<PartyController>
{
    public static PlayerCTL player;
    public static Inventory inventoryG;

    #region Main Method
    private void Update()
    {
        if (player == null)
            player = transform.GetChild(0).gameObject.GetComponent<PlayerCTL>();

        if (inventoryG == null)
            inventoryG = new Inventory { Gold = player.playerdata.otherStats.gold };
        player.playerdata.otherStats.gold = inventoryG.Gold;
    }
    #endregion

    #region Resurb Method
    public void Respawn(int lostRateExp,int lostRateGold)
    {
        player.isAlve = true;
        player.gameObject.SetActive(true);
        GameManager.instance.RespawnAfterDie(lostRateExp);
        inventoryG.IncreaseGold(-(int)((lostRateGold * inventoryG.Gold) / 100));
    }
    public void FullRestore()
    {
        var _init = player.playerdata.basicStats;
        player.Health.InitValue(Mathf.CeilToInt(_init.health), Mathf.CeilToInt(_init.health));
        player.Mana.InitValue(Mathf.CeilToInt(_init.mana), Mathf.CeilToInt(_init.mana));
    }
    public static void IncreaseCoin(int amount)
    {
        inventoryG.Gold = Mathf.Clamp(inventoryG.Gold + amount, 0, Int32.MaxValue);
    }
    public static void AddExperience(float amount) => GameManager.instance.AddExperience(amount);
    #endregion
}
