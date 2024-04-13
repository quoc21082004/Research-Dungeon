using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class ExplosionSpell : MonoBehaviour, ISpell
{
    private ActiveAbility activeAbility;
    public float explosionRadius;
    float critDamage;
    public Collider2D[] colliders;
    public void KickOff(ActiveAbility ability, Vector2 direction, Quaternion rot)
    {
        activeAbility = ability;
        transform.position = PartyController.player.transform.position;
        transform.parent = PartyController.player.transform;
        Explore();
    }
    private float CaculateDamage()
    {
        critDamage = Random.Range(PartyController.player.playerdata.basicAttack.minCritDamage, PartyController.player.playerdata.basicAttack.maxCritDamage);
        float totalDamage = activeAbility.skillInfo.baseDamage + PartyController.player.playerdata.basicAttack.wandDamage;
        return totalDamage;
    }
    public void Explore()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, LayerMaskHelper.layerMaskEnemy);
        if (colliders.Length == 0)
            return;
        foreach(var colli in colliders)
        {
            if (colli.gameObject.TryGetComponent<EnemyHurt>(out EnemyHurt enemy))
            {
                int rate = Random.Range(0, 101);
                bool isCrit = PartyController.player.playerdata.basicAttack.critChance > rate ? true : false;
                float totalDamage = 0;
                if (isCrit)
                    totalDamage = CaculateDamage() * critDamage;
                else
                    totalDamage = CaculateDamage();
                enemy.TakeDamage(totalDamage, isCrit);
            }
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
    
}
