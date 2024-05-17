using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBurstZone : ExplosionBuilet
{
    public float explosionRange;
    public GameObject explosionBurstprefab;
    float critDamage;

    protected override void HitTarget()
    {
        base.HitTarget();
        GameObject clone = PoolManager.instance.Release(explosionBurstprefab, transform.position, Quaternion.identity);
        if (clone != null)
        {
            Explosion(transform.position);
            AudioManager.instance.PlaySfx("Explosion");
        }
    }
    private float CaculateDamage()
    {
        var _playerSO = PartyController.player.playerdata.basicAttack;
        critDamage = _playerSO.GetCritDMG();
        float totalDamage = ((activeAbility.skillInfo.baseDamage + _playerSO.GetDamage()) * Random.Range(240, 270)) / 115f;
        return totalDamage;
    }
    private void Explosion(Vector2 postion)
    {
        var colliders = Physics2D.OverlapCircleAll(postion, explosionRange, LayerMaskHelper.layerMaskEnemy);
        if (colliders.Length == 0)
            return;
        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                bool isCrit = Random.Range(30, 40) > Random.Range(0, 101) ? true : false;
                float totalDamage = isCrit ? CaculateDamage() * critDamage : CaculateDamage();
                IDamagable damage = collider.gameObject.GetComponent<IDamagable>();
                if (damage != null)
                    damage.TakeDamage(totalDamage, isCrit);
            }
        }
    }
}
