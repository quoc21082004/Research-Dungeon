using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public EnemySO enemystat;
    public PlayerCTL player;
    Rigidbody2D myrigid;
    [HideInInspector] public EnemyHurt enemyhurt;
    [HideInInspector] public SpriteRenderer mySR;
    [HideInInspector] public Animator myanim;
    [HideInInspector] public float maxhealth, health, damage, defense, level;
    [HideInInspector] public bool isDead, turnOnAlert;
    [HideInInspector] public EnemyMood mood;

    protected float timer, alertrange, attacktimer, range, builetspeed;
    protected bool isBoss, isAlert, isAttack, isWithIn;
    [Header("Alert")]
    public GameObject alertprefab;
    public Vector3 alertOffSet;
    EnemyUI enemyUI;
    public float knockTime;
    protected bool canUse;
    protected NavMeshAgent myagent;
    protected ActiveAbility ability;

    [Space]
    public UnityEvent OnTakeDamageEvent;
    public UnityEvent OnDieEvent;
    protected abstract void CheckDistance();
    protected virtual void OnEnable()
    {
        enemyhurt = GetComponent<EnemyHurt>();
        level = (int)Random.Range(GameManager.instance.level - 3, GameManager.instance.level + 3);
        maxhealth = enemystat.heath + (level * enemystat.growstats.healthGrow);
        health = maxhealth;
        defense = enemystat.defense + (level * enemystat.growstats.defenseGrow);
        damage = enemystat.damage + (level * enemystat.growstats.damageGrow);
    }
    protected virtual void Awake()
    {
        attacktimer = enemystat.attackTimer;
        alertrange = enemystat.alertRange;
        range = enemystat.range;
        builetspeed = enemystat.builetSpeed;
        isDead = enemystat.isDead;
        isAlert = enemystat.isAlert;
        isAttack = enemystat.isAttack;
        ability = GetComponent<ActiveAbility>();
        player = PartyController.player;
        myanim = GetComponent<Animator>();
        mySR = GetComponent<SpriteRenderer>();
        enemyUI = GetComponentInChildren<EnemyUI>();
        myrigid = GetComponent<Rigidbody2D>();

        myagent = GetComponent<NavMeshAgent>();
        myagent.updateRotation = false;
        myagent.updateUpAxis = false;
    }
    public virtual void FlipCharacter()
    {
        if (transform.position.x > player.transform.position.x)
            mySR.flipX = false;
        else if (transform.position.x < player.transform.position.x)
            mySR.flipX = true;
    }
    public void AlertOn()
    {
        if (!isAlert)
            return;
        if (isAlert)
        {
            if (!turnOnAlert)
            {
                GameObject alertClone = PoolManager.instance.Release(alertprefab, transform.position + alertOffSet, Quaternion.identity);
                alertClone.transform.parent = transform;
            }
            turnOnAlert = true;
            if (enemyUI != null)
            {
                enemyUI.gameObject.SetActive(true);
                enemyUI.enabled = true;
            }
        }
    }
    public void AlertOff()
    {
        if (isAlert)
            return;
        enemyUI.gameObject.SetActive(false);
        enemyUI.enabled = false;
    }
    public void GetKnockBack(Transform damageSouce, float thurstKnock, Transform parent)
    {
        Vector2 dir = (transform.position - damageSouce.position).normalized * thurstKnock * myrigid.mass;
        myrigid.AddForce(dir, ForceMode2D.Impulse);
        GameObject ps = PoolManager.instance.Release(AssetManager.instance.assetData.knockbackEffect.gameObject, parent.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        StartCoroutine(CourtineKnock());
    }
    IEnumerator CourtineKnock()
    {
        yield return new WaitForSeconds(knockTime);
        myrigid.velocity = Vector2.zero;
    }
}
