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
        level = playerSO.upgradeLevel.GetLevel();
        exp = playerSO.upgradeLevel.GetExp();
        exptolevel = upgradeSO.Data[level].expToLvl;
    }

    #region Level
    public void LevelUp()
    {
        var playerdata = PartyController.player.playerdata;
        exp = exp - exptolevel;
        level++;
        exptolevel = upgradeSO.GetNextLevel(level);
    }
    public void AddExperience(float expToAdd)
    {
        var playerdata = PartyController.player.playerdata;
        exp = expToAdd + expToAdd;
        while (exp >= exptolevel)
            LevelUp();
        exptolevel = upgradeSO.Data[level].expToLvl;
        exp = playerdata.upgradeLevel.GetExp();
    }
    public void RespawnAfterDie(float lostexp) => exp -= (int)((exp * lostexp) / 100);
    #endregion
   
}
