using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSpell : MonoBehaviour, ISpell
{
    private ActiveAbility activeAbility;
    public GameObject lightprefab;
    public float lightRadius;
    public float duration;
    public Vector3 spellOffSet;
    private float lightInterval = 1.5f;
    Collider2D[] colliders;
    float critDamage;
    private float CaculateDamage()
    {
        critDamage = Random.Range(PartyController.player.playerdata.basicAttack.minCritDamage, PartyController.player.playerdata.basicAttack.maxCritDamage);
        float totalDamage = ((activeAbility.skillInfo.baseDamage + PartyController.player.playerdata.basicAttack.wandDamage) * Random.Range(150f, 200f)) / 100;
        return totalDamage;
    }
    public void KickOff(ActiveAbility ability, Vector2 direction, Quaternion rot)
    {
        activeAbility = ability;
        transform.position = PartyController.player.transform.position + spellOffSet;
        transform.parent = PartyController.player.transform;
        StartCoroutine(AttackCourtine());
    }
    private IEnumerator AttackCourtine()
    {
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, lightRadius, LayerMaskHelper.layerMaskEnemy);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<EnemyHurt>(out EnemyHurt enemy))
                {
                    if (enemy != null)
                    {
                        GameObject cloneLighting = PoolManager.instance.Release(lightprefab, enemy.transform.position + spellOffSet, Quaternion.Euler(0, 0, 40f));
                        if (cloneLighting != null)
                        {
                            bool isCrit = PartyController.player.playerdata.basicAttack.critChance > Random.Range(0, 101) ? true : false;
                            if (isCrit)
                                enemy.TakeDamage(CaculateDamage() * critDamage, isCrit);
                            else
                                enemy.TakeDamage(CaculateDamage(), isCrit);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(lightInterval);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lightRadius);
    }
}
