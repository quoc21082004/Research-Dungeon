
using System.Collections;
using UnityEngine;
public class Boar : Enemy
{
    float Distance;
    protected override void Awake()
    {
        base.Awake();
        isAlert = false;
        timer = 0;
    }

    private void Update()
    {
        bool checkDistance = true ? player != null : player == null;
        if (checkDistance)
            Distance = Vector3.Distance(player.transform.position, transform.position);
        else
            Distance = 0;
        float distanceAlert = Distance;
        isAlert = true ? distanceAlert <= alertrange : distanceAlert > alertrange;
        if (!isAlert)
            AlertOff();
        else if (isAlert)
        {
            AlertOn();
            if (Distance <= range) // Attack
            {
                timer += Time.deltaTime;
                if (timer > attacktimer)
                {
                    StartCoroutine(AttackDelay());
                    timer = 0;
                }
            }
            else if (Distance > range && isAttack) // aatack
                myagent.SetDestination(player.transform.position);
        }
        FlipCharacter();
    }

    IEnumerator AttackDelay()
    {
        isAttack = true;
        yield return new WaitForSeconds(1.0f);
        myanim.SetTrigger("Attack");
        player.GetComponent<PlayerHurt>().TakeDamage(damage, false);
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.TryGetComponent<PlayerCTL>(out var player))
            {
                player.GetComponent<PlayerHurt>().TakeDamage(2, false);
            }
        }
    }
    protected override void CheckDistance()
    {
        throw new System.NotImplementedException();
    }
}
