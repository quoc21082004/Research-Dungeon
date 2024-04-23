using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpikeSpell : MonoBehaviour, ISpell
{
    [SerializeField] Transform damageZone;
    Animator myanim;
    new Collider2D collider;
    [SerializeField] GameObject explosionprefab;
    private ActiveAbility activeAbility;
    [Header("Skill Info")]
    [SerializeField] float damageRadius;
    [SerializeField] float explosionRadius;
    [SerializeField] float zoneScale;
    [SerializeField] private float indicateDuration;
    [SerializeField] private float waitAfterIndicated;
    [SerializeField] private float timeSetAnimation;
    [SerializeField] private float spikeLifeTime;
    private void Awake()
    {
        myanim = GetComponentInChildren<Animator>();
        collider = GetComponent<Collider2D>();
    }
    private void OnDisable() => collider.enabled = false;
    public void KickOff(ActiveAbility ability, Vector2 position, Quaternion rot)
    {
        activeAbility = ability;
        transform.position = position;
        StartCoroutine(SpellCourtine(ability));
    }
    private IEnumerator SpellCourtine(ActiveAbility ability)
    {
        damageZone.localScale = Vector3.zero;
        damageZone.DOScale(new Vector3(1f, 1f, 1f) * zoneScale, indicateDuration);
        yield return new WaitForSeconds(indicateDuration);
        yield return new WaitForSeconds(waitAfterIndicated);
        //AudioManager.instance.PlaySfx("SpikeUp");
        myanim.Play("SpikeUp");
        yield return new WaitForSeconds(timeSetAnimation);
        collider.enabled = true;
        DealDamageRange(ability, damageRadius);
        yield return new WaitForSeconds(spikeLifeTime);
        collider.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Explosion(ability);
    }
    private void DealDamageRange(ActiveAbility ability, float radius)
    {
        var colliders = Physics2D.OverlapCircleAll(damageZone.position, radius, LayerMaskHelper.layerMaskPlayer);
        if (colliders.Length == 0)
            return;
        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<PlayerHurt>(out PlayerHurt target))
            {
                target.TakeDamage(ability.skillInfo.baseDamage, false);
            }
        }
    }
    private void Explosion(ActiveAbility ability)
    {
        GameObject clone = PoolManager.instance.Release(explosionprefab, damageZone.transform.position, Quaternion.identity);
        DealDamageRange(ability, explosionRadius);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(damageZone.position, damageRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(damageZone.position, explosionRadius);
    }
}
