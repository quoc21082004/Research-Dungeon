using System.Collections;
using UnityEngine;
public class Scorpion : EnemyRange
{
    public Transform[] builetPos;
    public GameObject builetprefab;
    protected override void Awake()
    {
        base.Awake();
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

        builetPos[0].rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        builetPos[1].rotation = Quaternion.AngleAxis(angle + 35, Vector3.forward);
        builetPos[2].rotation = Quaternion.AngleAxis(angle - 35, Vector3.forward);
    }
    private void Attack()
    {
        Direction();
        foreach (var item in builetPos)
        {
            builetprefab.gameObject.GetComponent<EnemyBuilet>().realdamage =damage / 2.5f;
            GameObject clone = PoolManager.instance.Release(builetprefab, item.transform.position, item.transform.rotation);
            Rigidbody2D crb = clone.GetComponent<Rigidbody2D>();
            crb.AddForce(item.up * builetspeed, ForceMode2D.Impulse);
            crb.velocity = Vector2.ClampMagnitude(crb.velocity, builetspeed);
        }
    }
    protected override IEnumerator attackDelay()
    {
        for (int i = 0; i < 3; i++)
        {
            Attack();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
