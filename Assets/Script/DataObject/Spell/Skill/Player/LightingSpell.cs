using System.Collections;
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
        var _playerSO = PartyController.player.playerdata.basicAttack;
        critDamage = _playerSO.GetCritDMG();
        float totalDamage = ((activeAbility.skillInfo.baseDamage + _playerSO.GetDamage() * Random.Range(150f, 200f)) / 100);
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
                    GameObject cloneLighting = PoolManager.instance.Release(lightprefab, enemy.transform.position + spellOffSet, Quaternion.Euler(0, 0, 40f));
                    if (cloneLighting != null)
                    {
                        var _playerSO = PartyController.player.playerdata.basicAttack;
                        bool isCrit = _playerSO.GetCrit() > Random.Range(0, 101) ? true : false;
                        float totalDMG = isCrit ? CaculateDamage() * _playerSO.GetCritDMG() : CaculateDamage();
                        IDamagable damage = collider.gameObject.GetComponent<IDamagable>();
                        if (damage != null)
                            damage.TakeDamage(totalDMG, isCrit);
                    }
                }
            }
            yield return new WaitForSeconds(lightInterval);
        }
    }
}
