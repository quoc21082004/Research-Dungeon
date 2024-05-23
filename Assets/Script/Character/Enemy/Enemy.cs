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

    #endregion

    #region Variable
    public EnemySO enemySO;
    protected float timer, alertrange, attacktimer, range, builetspeed;
    protected bool isBoss, isAlert, isAttack, isWithIn;
    public float knockTime;
    protected bool canUse;
    [HideInInspector] public int damage, defense, level;
    [HideInInspector] public bool isDead;
    [HideInInspector] public EnemyMood mood;

    #endregion

    #region Main Method
    public virtual void Start()
    {
        myagent.updateRotation = false;
        myagent.updateUpAxis = false;
    }
    protected virtual void Awake()
    {
        myagent = GetComponent<NavMeshAgent>();
        ability = GetComponent<ActiveAbility>();
        player = PartyController.player;
        myanim = GetComponent<Animator>();
        mySR = GetComponent<SpriteRenderer>();
        myrigid = GetComponent<Rigidbody2D>();
        enemyhurt = GetComponent<EnemyHurt>();
    }
    protected virtual void OnEnable()
    {
        level = Random.Range(GameManager.instance.level + 1, GameManager.instance.level + 8);
        Health.InitValue(enemySO.GetHP() + (level * enemySO.GetGrowStat().GetHealthGrown()), enemySO.GetHP() + (level * enemySO.GetGrowStat().GetHealthGrown()));
        defense = enemySO.GetDef() + (level * enemySO.GetGrowStat().GetDefenseGrown());
        damage = enemySO.GetHP() + (level * enemySO.GetGrowStat().GetDamageGrown());


        attacktimer = enemySO.GetAttackTimer();
        alertrange = enemySO.GetAlertRange();
        range = enemySO.GetRange();
        builetspeed = enemySO.GetBuiletSpeed();
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
    protected abstract void CheckDistance();

    #endregion 

}
