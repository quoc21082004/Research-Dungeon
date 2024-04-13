using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZoneSpellCenter : MonoBehaviour, ISpell
{
    public Transform[] damageZonePos;
    public GameObject damageZoneprefab;
    public void KickOff(ActiveAbility ability, Vector2 direction, Quaternion rot)
    {
        this.transform.position = transform.position;
        foreach (var pos in damageZonePos)
        {
            if (PoolManager.instance.Release(damageZoneprefab).gameObject.TryGetComponent<DamageZoneSpell>(out DamageZoneSpell spell))
            {
                spell.KickOff(ability, pos.transform.position, Quaternion.identity);
            }
        }
    }
}
