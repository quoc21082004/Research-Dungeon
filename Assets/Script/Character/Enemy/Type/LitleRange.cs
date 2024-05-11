using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LittleRange : EnemyRange
{
    public GameObject builetprefab;
    public Transform MuzzlePoint;
    protected override void Awake()
    {
        base.Awake();
        timer = 0;
        isAlert = false;
        moveRange = 2f;
    }
    private void FixedUpdate()
    {
        CheckDistance();
        DistanceAttack();
    }
    protected override void Direction()
    {
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        MuzzlePoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    protected override IEnumerator attackDelay()
    {
        Direction();
        builetprefab.gameObject.GetComponent<EnemyBuilet>().realdamage = damage/ 1.5f;
        GameObject clone = PoolManager.instance.Release(builetprefab, MuzzlePoint.position, MuzzlePoint.rotation);
        Rigidbody2D crb = clone.GetComponent<Rigidbody2D>();
        crb.AddForce(MuzzlePoint.up * builetspeed, ForceMode2D.Impulse);
        crb.velocity = Vector2.ClampMagnitude(crb.velocity, builetspeed);
        yield return null;
    }
}