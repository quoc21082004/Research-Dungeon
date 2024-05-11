using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ActiveAbility : MonoBehaviour , IActiveAbility 
{
    public SkillSO skillInfo;
    public float CastDelay => skillInfo.baseCastDelay;
    public float MaxUseRange => skillInfo.maxUseRange;
    public bool IsEnoughMana() => PartyController.player.Mana.currentValue >= skillInfo.baseCostMana;
    public Respond TryUse()
    {
        if (!IsEnoughMana())
            return Respond.NotEnoughMana;
        StartSpell();
        return Respond.Success;
    }
    private void StartSpell()
    {
        if (!IsEnoughMana())   
            return;
        //PartyController.player.Mana.currentValue -= skillInfo.baseCostMana;
        PartyController.player.Mana.currentValue -= Mathf.CeilToInt(skillInfo.baseCostMana);
        if (PoolManager.instance.Release(skillInfo.spellPrefab).TryGetComponent<ISpell>(out var spell))
            spell.KickOff(this, transform.position - PartyController.player.transform.position,Quaternion.identity);
    }
}
