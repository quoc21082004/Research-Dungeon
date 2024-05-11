using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosionZone : MonoBehaviour, ISpell
{
    Animator myanim;
    private ActiveAbility abilitySpell;
    Collider2D[] colliders;
    public float prepareTime;
    public float durationTime;
    public float damageTimeEclipse;
    public float damageRange;
    float critDamage;
    private void Awake()
    {
        myanim = GetComponent<Animator>();
    }
    public void KickOff(ActiveAbility ability, Vector2 direction, Quaternion rot)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos) ;
        abilitySpell = ability;
        direction = Vector2.ClampMagnitude(direction, abilitySpell.MaxUseRange);
        transform.position = mousePos - (Vector3)direction;
        StartCoroutine(StartPrepare());
    }
    private float CaculateDamage()
    {
        critDamage = Random.Range(PartyController.player.playerdata.basicAttack.minCritDamage, PartyController.player.playerdata.basicAttack.maxCritDamage);
        float ratePercent = Random.Range(150f, 170f);
        float totalDamage = (((abilitySpell.skillInfo.baseDamage + PartyController.player.playerdata.basicAttack.wandDamage * ratePercent) / 100)) * Random.Range(1.75f, 2.5f);
        return totalDamage;
    }
    private IEnumerator StartPrepare()
    {
        yield return new WaitForSeconds(prepareTime);
        myanim.Play("PoisonZoneDamage");
        var endTime = Time.time + durationTime;
        while (Time.time <= endTime) 
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, damageRange, LayerMaskHelper.layerMaskEnemy);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<EnemyHurt>(out var enemy))
                {
                    bool isCrit = PartyController.player.playerdata.basicAttack.critChance > Random.Range(0, 101) ? true : false;
                    if (isCrit)
                        enemy.TakeDamage(CaculateDamage() * critDamage, isCrit);
                    else
                        enemy.TakeDamage(CaculateDamage(), isCrit);
                }
            }
            yield return new WaitForSeconds(damageTimeEclipse);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position, damageRange);
    }
}
