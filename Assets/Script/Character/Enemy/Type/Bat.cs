using System.Collections;
using UnityEngine;
public class Bat : EnemyRange
{
    public GameObject builetprefab;
    GameObject clone;
    public Transform[] builetPos;
    protected override void Awake()
    {
        base.Awake();
        isAlert = false;
        timer = 0;
        moveRange = 2f;
    }
    private void FixedUpdate()
    {
        CheckDistance();
        DistanceAttack();
    }
    private void Attack()
    {
        foreach (var direct in builetPos)
        {
            builetprefab.gameObject.GetComponent<EnemyBuilet>().realdamage = damage/ 4f;
            clone = PoolManager.instance.Release(builetprefab, direct.position, direct.rotation);
            Rigidbody2D crb = clone.GetComponent<Rigidbody2D>();
            crb.AddForce(direct.up * builetspeed, ForceMode2D.Impulse);
            crb.velocity = Vector2.ClampMagnitude(crb.velocity, builetspeed);
        }
    }
    protected override void Direction()
    {
        Vector3 direction = player.transform.position - transform.position; // 5-3
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        foreach (var item in builetPos)
        {
            angle -= 45;
            item.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // angle 45 - 90 - 135 - 180 - 225 - 270 - 315 - 360
        }
    }
    protected override IEnumerator attackDelay()
    {
        Direction();
        myagent.enabled = false;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 3; i++)
        {
            Attack();
            yield return new WaitForSeconds(0.2f);
        }
        myagent.enabled = true;
    }
}