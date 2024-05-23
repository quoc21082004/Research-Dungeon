using System.Collections;
using UnityEngine;

public class BossStormSpell : MonoBehaviour, ISpell
{
    const string Animation_Hash = "Storm";
    private Animator myanim;
    private Rigidbody2D myrigid;
    public float startDelay;
    public float lifeTime;
    public float speed;
    public float damagePerInterval;
    public Vector2 damageZoneSize;
    Collider2D[] colliders;
    private ActiveAbility activeAbility;
    private float endTime;
    private Transform target;
    private Coroutine stormCoroutine;
    private void Awake()
    {
        myanim = GetComponent<Animator>();
        myrigid = GetComponent<Rigidbody2D>();
    }
    public void KickOff(ActiveAbility ability, Vector2 direction, Quaternion rot)
    {
        activeAbility = ability;
        target = PartyController.player.transform;
        transform.position = direction;
        if (stormCoroutine != null)
            StopCoroutine(stormCoroutine);
        StartCoroutine(StormCouritne(ability));
    }
    private IEnumerator StormCouritne(ActiveAbility ability)
    {
        yield return new WaitForSeconds(startDelay);
        myanim.Play(Animation_Hash);
        endTime = Time.time + lifeTime;
        StartCoroutine(DamageCourtine(ability));
        while (Time.time < endTime)
        {
            var direction = (target.position - transform.position).normalized;
            myrigid.velocity = direction * speed;
            yield return new WaitForSeconds(0.5f);
        }
        gameObject.SetActive(false);
    }
    private IEnumerator DamageCourtine(ActiveAbility ability)
    {
        while (Time.time < endTime)
        {
            colliders = Physics2D.OverlapBoxAll(transform.position, damageZoneSize, 0, LayerMaskHelper.layerMaskPlayer);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<PlayerCTL>(out var target))
                {
                    IDamagable damage = collider.gameObject.GetComponent<IDamagable>();
                    if (damage != null)
                        damage.TakeDamage(activeAbility.skillInfo.baseDamage, false);
                }
            }
            yield return new WaitForSeconds(damagePerInterval);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, damageZoneSize);
    }
}
