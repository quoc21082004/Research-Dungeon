using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingMelee : EnemyMelee
{
    bool isDMG;
    protected override void Awake()
    {
        base.Awake();
        isAlert = false;
        isDMG = false;
        timer = 0;
    }
    private void FixedUpdate()
    {
        CheckDistance();
        DistanceAttack();
    }
    protected override IEnumerator attackDelay()
    {
        isAttack = true;
        yield return new WaitForSeconds(1.0f);
        myanim.SetTrigger("Attack");
        yield return new WaitForSeconds(1.3f);
        isAttack = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerCTL>(out var player))
        {
            IDamagable _damage = collision.gameObject.GetComponent<IDamagable>();
            if (_damage != null)
                _damage.TakeDamage(damage, false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerCTL>(out var player))
        {
            IDamagable _damage = collision.gameObject.GetComponent<IDamagable>();
            if (_damage != null)
            {
                if (isAttack)
                    if (!isDMG)
                    {
                        isDMG = true;
                        _damage.TakeDamage(damage, false);
                        StartCoroutine(dmgCD());
                    }
            }
        }
    }
    IEnumerator dmgCD()
    {
        yield return new WaitForSeconds(0.3f);
        isDMG = false;
    }
}