using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public float exp, exptolevel;
    [HideInInspector] public int level;
    public CharacterUpgradeSO upgradeSO;
    private void OnEnable()
    {
        //var playerdata = PartyController.player.playerdata;
        exp = PlayerPrefs.GetFloat("Exp"); //playerdata.upgradeLevel.exp;//PlayerPrefs.GetFloat("Exp");
        exptolevel = PlayerPrefs.GetFloat("Explevelup");//playerdata.upgradeLevel.expToLvl;//PlayerPrefs.GetFloat("Explevelup");
        level = PlayerPrefs.GetInt("level");//playerdata.upgradeLevel.level;//PlayerPrefs.GetInt("level");
    }
    public void LevelUp()
    {
        var playerdata = PartyController.player.playerdata;
        exp = exp - exptolevel;
        PartyController.player.playerdata.otherStats.skillPoint += 1;
        playerdata.upgradeLevel.expToLvl = playerdata.upgradeLevel.expToLvl * 1.2f;
        playerdata.upgradeLevel.level++;
    }
    public void AddExperience(float expToAdd)
    {
        var playerdata = PartyController.player.playerdata;
        exp = expToAdd + expToAdd + (expToAdd * PlayerPrefs.GetFloat("extraExp") / 100);
        while (exp >= exptolevel)
            LevelUp();
        exptolevel = playerdata.upgradeLevel.expToLvl;
        exp = playerdata.upgradeLevel.exp;
    }
    public void RespawnAfterDie(float lostexp) => exp -= (int)((exp * lostexp) / 100); 
}
