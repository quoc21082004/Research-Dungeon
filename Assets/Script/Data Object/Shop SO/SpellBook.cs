using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shop Item", menuName = "Shop/Spell Book")]  
public class SpellBook : Consumable
{
    public SkillSO spell;
    ActiveAbility ability;
    public static float CD;
    public override void Use()
    {
        if (ability == null)
            ability = PartyController.player.GetComponent<ActiveAbility>();
        CD = spell.baseCoolDown;
        base.Use();
        switch(type)
        {
            case SpellBookType.ExplosionCircle:
                spell.isUnlock = true;
                break;
            case SpellBookType.ExplosionBuilet:
                spell.isUnlock = true;
                break;
            case SpellBookType.PoisonZone:
                spell.isUnlock = true;
                break;
            case SpellBookType.LightingCircle:
                spell.isUnlock = true;
                break;
            case SpellBookType.ShieldZone:
                spell.isUnlock = true;
                break;
            default:
                break;
        }
        if (spell != null && ability != null) 
        {
            ability.skillInfo = spell;
            ability.TryUse();
        }
    }
    public static float GetSpellCooldown(SpellBookType type)
    {
        switch(type)
        {
            case SpellBookType.ExplosionCircle:
                return CD + 1;
            case SpellBookType.ExplosionBuilet:
                return CD + 2;
            case SpellBookType.PoisonZone:
                return CD + 4;
            case SpellBookType.LightingCircle:
                return CD + 8;
            case SpellBookType.ShieldZone:
                return CD + 10;
            default:
                return -1;
        }
    }
}
