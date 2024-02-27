using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public float exp, exptolevel;
    [HideInInspector] public int level;
    public PlayerSO playerdata;
    private void OnEnable()
    {
        exp = PlayerPrefs.GetFloat("Exp");
        exptolevel = PlayerPrefs.GetFloat("Explevelup");
        level = PlayerPrefs.GetInt("level");
    }
    public void AddExperience(float expToAdd)
    {
        PlayerPrefs.SetFloat("Exp", PlayerPrefs.GetFloat("Exp") + expToAdd + (expToAdd * PlayerPrefs.GetFloat("extraExp")) / 100);
        while (PlayerPrefs.GetFloat("Exp") >= PlayerPrefs.GetFloat("Explevelup"))
            LevelUp();
        exptolevel = PlayerPrefs.GetFloat("Explevelup");
        exp = PlayerPrefs.GetFloat("Exp");
        PlayerPrefs.Save();
    }
    public void LevelUp()
    {
        PlayerPrefs.SetFloat("Exp", PlayerPrefs.GetFloat("Exp") - PlayerPrefs.GetFloat("Explevelup"));
        PartyController.player.playerdata.levelUp.skillPoint += 1;
        PlayerPrefs.SetFloat("Explevelup", PlayerPrefs.GetFloat("Explevelup") * 1.20f);
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        level = PlayerPrefs.GetInt("level");
        PlayerPrefs.Save();
    }
    public void RespawnAfterDie(float lostexp) => exp -= (int)((exp * lostexp) / 100); 
}
