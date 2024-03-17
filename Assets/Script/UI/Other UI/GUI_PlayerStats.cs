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

    [Header("Upgrade")]
    [SerializeField] TextMeshProUGUI power_txt;
    [SerializeField] TextMeshProUGUI magic_txt;
    [SerializeField] TextMeshProUGUI vitality_txt;
    [SerializeField] TextMeshProUGUI haste_txt;
    [SerializeField] TextMeshProUGUI greed_txt;
    private void OnEnable()
    {
        UpdateStatsText();
    }
    private void UpdateStatsText()
    {
        var playerdata = PartyController.player.playerdata;

        // basic
        hp_txt.text = playerdata.basicStats.health.ToString("F0");
        mp_txt.text = playerdata.basicStats.mana.ToString("F0");
        atk_txt.text = playerdata.basicAttack.wandDamage.ToString("F0");
        def_txt.text = playerdata.basicStats.defense.ToString("F0");
        spd_txt.text = playerdata.basicStats.movementSpeed.ToString("F1");
        level_txt.text = playerdata.upgradeLevel.level.ToString("F0");
        // dmg
        DMG_txt.text = playerdata.extraBuff.percentDamage.ToString("F2") + "%";
        crit_txt.text = playerdata.basicAttack.critChance.ToString("F2") + "%";
        critDMG_txt.text = playerdata.basicAttack.maxCritDamage.ToString("F2") + "%";
        atkSpd_txt.text = playerdata.basicAttack.attackSpeed.ToString("F2");
        // regen
        hpRegen_txt.text = playerdata.basicStats.healthRegen.ToString("F2") + "%";
        mpRegen_txt.text = playerdata.basicStats.manaRegen.ToString("F2") + "%";
        // level upgrade

        power_txt.text = playerdata.upgrade.powerlevel.ToString();
        magic_txt.text = playerdata.upgrade.magiclevel.ToString();
        vitality_txt.text = playerdata.upgrade.vitalitylevel.ToString();
        haste_txt.text = playerdata.upgrade.hastelevel.ToString();
        greed_txt.text = playerdata.upgrade.greedlevel.ToString();

    }
}