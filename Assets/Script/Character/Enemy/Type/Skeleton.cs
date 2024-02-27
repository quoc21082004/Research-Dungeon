using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
            player.gameObject.GetComponent<PlayerHurt>().TakeDamage(enemyhurt.CaculateDMG(damage), false);
        yield return new WaitForSeconds(1f);
        isAttack = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<PlayerHurt>().TakeDamage(damage, false);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, alertrange);
    }
}
