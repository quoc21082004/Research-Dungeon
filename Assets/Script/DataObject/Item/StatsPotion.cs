using UnityEngine;
[CreateAssetMenu(fileName = "Shop Item", menuName = "Shop/Potion Stat")]
public class StatsPotion : Consumable
{
    public StatsType statsType;
    public override void Use()
    {
        var _playerdata = PartyController.player.playerdata;
        float _currentValue = 0;
        switch(statsType)
        {
            case StatsType.HP:
                _currentValue = _playerdata.basicStats.GetHealth() + value;
                _playerdata.basicStats.SetHealth((int)_currentValue);
                break;
            case StatsType.MP:
                _currentValue = _playerdata.basicStats.GetMana() + value;
                _playerdata.basicStats.SetMana((int)_currentValue);
                break;
            case StatsType.ATK:
                _currentValue = _playerdata.basicAttack.GetDamage() + value;
                _playerdata.basicAttack.SetDamage((int)_currentValue);
                break;
            case StatsType.DEF:
                _currentValue = _playerdata.basicStats.GetDef() + value;
                _playerdata.basicStats.SetDef((int)_currentValue);
                break;
            case StatsType.CRIT:
                _currentValue = _playerdata.basicAttack.GetCrit() + value;
                _playerdata.basicAttack.SetCrit(_currentValue);
                break;
            case StatsType.CRITDMG:
                _currentValue = _playerdata.basicAttack.GetCritDMG() + value;
                _playerdata.basicAttack.SetCritDMG(_currentValue);
                break;
            default:
                break;
        }
    }
}