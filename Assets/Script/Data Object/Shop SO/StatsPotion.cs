using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shop Item", menuName = "Shop/Potion Stat")]
public class StatsPotion : Consumable
{
    public StatsType statsType;
    public override void Use()
    {
        switch(statsType)
        {
            case StatsType.HP:
                PartyController.player.gameObject.GetComponent<Player>().playerdata.basicStats.health += value;
                break;
            case StatsType.MP:
                PartyController.player.gameObject.GetComponent<Player>().playerdata.basicStats.mana += value;
                break;
            case StatsType.ATK:
                PartyController.player.gameObject.GetComponent<Player>().playerdata.basicAttack.wandDamage += value;
                break;
            case StatsType.DEF:
                PartyController.player.gameObject.GetComponent<Player>().playerdata.otherStats.damageReduction += (int)value;
                break;
            case StatsType.CRIT:
                PartyController.player.gameObject.GetComponent<Player>().playerdata.basicAttack.critChance += value;
                break;
            case StatsType.CRITDMG:
                PartyController.player.gameObject.GetComponent<Player>().playerdata.basicAttack.maxCritDamage += value;
                PartyController.player.gameObject.GetComponent<Player>().playerdata.basicAttack.minCritDamage += value;
                break;
            default:
                break;
        }
    }
}