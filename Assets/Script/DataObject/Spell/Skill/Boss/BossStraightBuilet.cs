using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStraightBuilet : MonoBehaviour, ISpell
{
    Rigidbody2D myrigid;
    [SerializeField] float moveSpeed;
    public float lifeTime;
    protected ActiveAbility activeAbility;
    Transform target;
    private Coroutine lifeCheckCoroutine;
    private void OnEnable()
    {
        target = PartyController.player.GetComponent<Transform>();
        myrigid = GetComponent<Rigidbody2D>();
    }
    public void KickOff(ActiveAbility ability, Vector2 dir, Quaternion rot)
    {
        Vector2 distance = ((Vector2)target.transform.position - dir).normalized;
        activeAbility = ability;
        transform.position = ability.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        myrigid.velocity = moveSpeed * dir;
        if (lifeCheckCoroutine != null)
            StopCoroutine(lifeCheckCoroutine);
        StartCoroutine(LifeCheckCourtine());
    }
    private IEnumerator LifeCheckCourtine()
    {
        var lifeTimeInterval = 0.2f;
        Vector3 startPos = transform.position;
        var endTime = Time.time + lifeTime;
        while (Time.time <= endTime)
        {
            var distance = Vector3.Distance(transform.position, startPos);
            if (distance > activeAbility.MaxUseRange)
                break;
            yield return new WaitForSeconds(lifeTimeInterval);
        }
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerCTL>(out var enemy))
        {
            IDamagable damage = collision.gameObject.GetComponent<IDamagable>();
            if (damage != null)
                damage.TakeDamage(activeAbility.skillInfo.baseDamage, false);
            gameObject.SetActive(false);
        }
    }
}
