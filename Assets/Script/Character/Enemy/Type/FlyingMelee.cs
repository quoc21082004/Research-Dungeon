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
        if (collision.gameObject.tag == "PlayerCTL")
        {
            if (collision.gameObject.TryGetComponent<PlayerCTL>(out PlayerCTL player))
            {
                player.GetComponent<PlayerHurt>().TakeDamage(damage, false);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerCTL")
        {
            if (collision.gameObject.TryGetComponent<PlayerCTL>(out PlayerCTL player))
                if (isAttack)
                    if (!isDMG)
                    {
                        isDMG = true;
                        player.GetComponent<PlayerHurt>().TakeDamage(enemyhurt.CaculateDMG(damage), false);
                        StartCoroutine(dmgCD());
                    }
        }
    }
    IEnumerator dmgCD()
    {
        yield return new WaitForSeconds(0.3f);
        isDMG = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, alertrange);
    }
    protected override void UntimateEnemyMelee()
    {

    }
}
