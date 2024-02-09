﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class EnemyMelee : Enemy
{
    protected float distance;
    public GameObject ultiprefab;
    protected override void OnEnable()
    {
        base.OnEnable();
        mood = EnemyMood.Normal;
        canUse = false;
    }
    protected override void CheckDistance()
    {
        bool checkDistance = true ? player != null : player == null;
        if (checkDistance)
            distance = Vector3.Distance(player.transform.position, transform.position);
        else
            distance = 0;
        float alertDis = distance;
        isAlert = true ? alertDis <= alertrange : alertDis > alertrange;
    }
    protected virtual void DistanceAttack()
    {
        #region             alert - distance
        if (!isAlert)
            AlertOff();
        else if (isAlert)
        {
            AlertOn();
            if (distance <= range)
            {
                isWithIn = true;
                timer += Time.deltaTime;
                if (timer >= attacktimer)
                {
                    StartCoroutine(attackDelay());
                    timer = 0;
                }
            }
            else if (distance > range && !isAttack)
            {
                isWithIn = false;
                myagent.SetDestination(player.transform.position);
            }
        }
        if (!isWithIn)
            myanim.SetBool("Run", true);
        else
            myanim.SetBool("Run", false);
        FlipCharacter();
        #endregion
        if ((mood == EnemyMood.End) && !canUse) 
        {
            UntimateEnemyMelee();
            canUse = true;
        }
    }
    protected abstract IEnumerator attackDelay();
    protected virtual void UntimateEnemyMelee()
    {
        if (PoolManager.instance.Release(ultiprefab, transform.position, Quaternion.identity).TryGetComponent<ISpell>(out var spell)) 
            spell.KickOff(ability, transform.position , Quaternion.identity);
    }
}