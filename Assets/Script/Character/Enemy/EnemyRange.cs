using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class EnemyRange : Enemy
{
    protected float distance;
    protected float moveRange;
    public GameObject ultiprefab;

    #region Main Method
    protected override void OnEnable()
    {
        base.OnEnable();
        canUse = false;
        mood = EnemyMood.Normal;
    }
    #endregion

    #region Resurb Method
    protected override void CheckDistance()
    {
        bool checkDistance = true ? player != null : player == null;
        if (checkDistance)
            distance = Vector3.Distance(player.transform.position, transform.position);
        else
            distance = 0;
    }
    protected virtual void DistanceAttack()
    {
        #region alert - distance
        float alertDis = distance;
        timer += Time.deltaTime;
        isAlert = true ? alertDis <= alertrange : alertDis > alertrange;
        if (!isAlert)
            AlertOff();
        else if (isAlert)
        {
            AlertOn();
            if (distance <= range)
            {
                if (timer >= attacktimer)
                {
                    StartCoroutine(attackDelay());
                    myagent.SetDestination(-player.transform.position);
                    timer = 0;
                }
            }
            else if (distance > range)
            {
                var newpos = player.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * moveRange;
                myagent.SetDestination(newpos);
            }
        }
        FlipCharacter(); // rotate
        #endregion
        if ((mood == EnemyMood.Medium || mood == EnemyMood.Advance) && !canUse)  // < 50
        {
            UltimateEnemyRange();
            canUse = true;
        }
    }
    protected abstract void Direction();
    protected abstract IEnumerator attackDelay();
    protected virtual void UltimateEnemyRange()
    {
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 270;
        Quaternion ultirotation = Quaternion.Euler(0f, 0f, angle);
        if (PoolManager.instance.Release(ultiprefab, transform.position, ultirotation).TryGetComponent<ISpell>(out var spell))
            spell.KickOff(ability, player.transform.position, Quaternion.identity);
    }
    #endregion

}