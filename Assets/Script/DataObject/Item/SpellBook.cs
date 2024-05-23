using UnityEngine;
[CreateAssetMenu(fileName = "Shop Item", menuName = "Shop/Spell Book")]  
public class SpellBook : Consumable
{
    public SpellBookType type;
    public SkillSO spell;
    private ActiveAbility ability;
    public override void Use()
    {
        if (ability == null)
            ability = PartyController.player.GetComponent<ActiveAbility>();
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
}
