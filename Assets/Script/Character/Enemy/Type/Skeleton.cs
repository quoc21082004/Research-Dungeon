using System.Collections;
using UnityEngine;
public class Skeleton : EnemyMelee
{
    protected override void Awake()
    {
        base.Awake();
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
        yield return new WaitForSeconds(0.15f);
        myanim.SetTrigger("Attack");
        if (distance <= range)
        {
            IDamagable _damage = player.GetComponent<IDamagable>();
            if (_damage != null)
                _damage.TakeDamage(damage, false);
        }
        yield return new WaitForSeconds(1f);
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
}
