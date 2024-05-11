using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{

    #region Components
    public PlayerCTL player;
    public Rigidbody2D myrigid;
    [HideInInspector] public EnemyHurt enemyhurt;
    [HideInInspector] public SpriteRenderer mySR;
    [HideInInspector] public Animator myanim;
    public StatusHandle Health = new();
    protected NavMeshAgent myagent;
    protected ActiveAbility ability;
    EnemyUI enemyUI;
    #endregion

    #region Variable
    public EnemySO enemystat;
    protected float timer, alertrange, attacktimer, range, builetspeed;
    protected bool isBoss, isAlert, isAttack, isWithIn;
    public float knockTime;
    protected bool canUse;
    [HideInInspector] public float damage, defense, level;
    [HideInInspector] public bool isDead, turnOnAlert;
    [HideInInspector] public EnemyMood mood;
    #endregion

    #region Main Method
    public virtual void Start()
    {
        myagent = GetComponent<NavMeshAgent>();
        myagent.updateRotation = false;
        myagent.updateUpAxis = false;
    }
    protected virtual void OnEnable()
    {
        enemyhurt = GetComponent<EnemyHurt>();
        level = (int)Random.Range(GameManager.instance.level - 3, GameManager.instance.level + 3);
        Health.InitValue((int)(enemystat.heath + (level * enemystat.growstats.healthGrow)), (int)(enemystat.heath + (level * enemystat.growstats.healthGrow)));
        defense = enemystat.defense + (level * enemystat.growstats.defenseGrow);
        damage = enemystat.damage + (level * enemystat.growstats.damageGrow);
    }
    protected virtual void Awake()
    {
        ability = GetComponent<ActiveAbility>();
        player = PartyController.player;
        myanim = GetComponent<Animator>();
        mySR = GetComponent<SpriteRenderer>();
        enemyUI = GetComponentInChildren<EnemyUI>();
        myrigid = GetComponent<Rigidbody2D>();

        attacktimer = enemystat.attackTimer;
        alertrange = enemystat.alertRange;
        range = enemystat.range;
        builetspeed = enemystat.builetSpeed;
        isDead = enemystat.isDead;
        isAlert = enemystat.isAlert;
        isAttack = enemystat.isAttack;
    }
    #endregion

    #region Resurb Method
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
    protected abstract void CheckDistance();

    #endregion 
}
