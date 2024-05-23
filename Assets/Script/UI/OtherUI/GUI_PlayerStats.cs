using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_PlayerStats : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] TextMeshProUGUI hp_txt;
    [SerializeField] TextMeshProUGUI mp_txt;
    [SerializeField] TextMeshProUGUI atk_txt;
    [SerializeField] TextMeshProUGUI def_txt;
    [SerializeField] TextMeshProUGUI spd_txt;
    [SerializeField] TextMeshProUGUI level_txt;
  

    [Header("DMG")]
    [SerializeField] TextMeshProUGUI DMG_txt;
    [SerializeField] TextMeshProUGUI crit_txt;
    [SerializeField] TextMeshProUGUI critDMG_txt;
    [SerializeField] TextMeshProUGUI atkSpd_txt;

    [Header("Recover")]
    [SerializeField] TextMeshProUGUI hpRegen_txt;
    [SerializeField] TextMeshProUGUI mpRegen_txt;

    private void OnEnable()
    {
        UpdateStatsText();
    }
    private void UpdateStatsText()
    {
        var playerdata = PartyController.player.playerdata;

        // basic
        hp_txt.text = playerdata.basicStats.GetHealth().ToString("F0");
        mp_txt.text = playerdata.basicStats.GetMana().ToString("F0");
        atk_txt.text = playerdata.basicAttack.GetDamage().ToString("F0");
        def_txt.text = playerdata.basicStats.GetDef().ToString("F0");
        spd_txt.text = playerdata.basicMovement.GetBaseSpeed().ToString("F1");
        level_txt.text = playerdata.upgradeLevel.GetLevel().ToString("F0");
        // dmg
        DMG_txt.text = playerdata.basicAttack.GetPercentDMG().ToString("F2") + "%";
        crit_txt.text = playerdata.basicAttack.GetCrit().ToString("F2") + "%";
        critDMG_txt.text = playerdata.basicAttack.GetCritDMG().ToString("F2") + "%";
        atkSpd_txt.text = playerdata.basicAttack.GetAttackSpeed().ToString("F2");

    }
}