using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public const string FILE_NAME = "PlayerStat.json";
    [HideInInspector] public float exp, exptolevel;
    [HideInInspector] public int level;

    // SO
    public PlayerSO playerSO;
    public CharacterUpgradeSO upgradeSO;
    public EquipmentUpgradeSO equipUpgradeSO;
    public GUI_PlayerStatus playerHUD;

    private void OnEnable()
    {
        level = playerSO.upgradeLevel.level;
        exp = playerSO.upgradeLevel.exp;
        exptolevel = upgradeSO.Data[level].expToLvl;
    }

    #region Level
    public void LevelUp()
    {
        var playerdata = PartyController.player.playerdata;
        exp = exp - exptolevel;
        playerdata.otherStats.skillPoint += 1;
        level++;
        exptolevel = upgradeSO.GetNextLevel(level);
    }
    public void AddExperience(float expToAdd)
    {
        var playerdata = PartyController.player.playerdata;
        exp = expToAdd + expToAdd;
        while (exp >= exptolevel)
            LevelUp();
        //exptolevel = playerdata.upgradeLevel.expToLvl;
        //exp = playerdata.upgradeLevel.exp;
        exptolevel = upgradeSO.Data[level].expToLvl;
        exp = playerdata.upgradeLevel.exp;
    }
    public void RespawnAfterDie(float lostexp) => exp -= (int)((exp * lostexp) / 100);
    #endregion
   
}
