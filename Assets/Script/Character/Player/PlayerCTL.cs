using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCTL : MonoBehaviour 
{

    #region Component Method
    [HideInInspector] public Rigidbody2D myrigid { get; private set; }
    [HideInInspector] public SpriteRenderer mySR { get; private set; }
    [HideInInspector] public Animator myanim { get; private set; }
    [HideInInspector] public PlayerHurt playerhurt { get; private set; }
    [HideInInspector] public PlayerCombat playercombat { get; private set; }
    [HideInInspector] public PlayerSO playerdata;
    [HideInInspector] public MouseFollow mousefollow { get; private set; }
    #endregion

    #region State Machine
    public PlayerStateMachine stateMachine { get; private set; }
    public IdleState idleState { get; private set; }
    public MoveState moveState { get; private set; }
    public SprintState sprintState { get; private set; }
    public DashState dashState { get; private set; }
    #endregion

    #region Variable 
    public StatusHandle Health = new();
    public StatusHandle Mana = new();

    public PlayerAnimationData animData;
    Collider2D[] pickup;
    [HideInInspector] public float rangePickup = 1.2f;
    [HideInInspector] public bool isAlve = true;
    public GameObject dashEffect;
    #endregion

    #region Main Method
    private void Awake()
    {
        playerdata = GameManager.instance.playerSO;

        var _health = Mathf.CeilToInt(playerdata.basicStats.GetHealth());
        Health.InitValue(_health, _health);

        var _mana = Mathf.CeilToInt(playerdata.basicStats.GetMana());
        Mana.InitValue(_mana, _mana);

        stateMachine = new PlayerStateMachine();
        idleState = new IdleState(this, stateMachine, "Idle");
        moveState = new MoveState(this, stateMachine, "Move");
        sprintState = new SprintState(this, stateMachine, "Sprint");
        dashState = new DashState(this, stateMachine, "Dash");
        animData.InitializeAnimation();

        myrigid = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        myanim = GetComponent<Animator>();
        playerhurt = GetComponentInParent<PlayerHurt>();
        playercombat = GetComponent<PlayerCombat>();
        mousefollow = GetComponentInChildren<MouseFollow>();
    }
    private void Start()
    {
        stateMachine.Initialize(idleState);
        stateMachine.ChangeState(idleState);
    }
    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
        if (isAlve)
        {
            PickUp();
            playerhurt.RegenRecover();
            playercombat.FindEnemy();
        }
    }
    private void FixedUpdate()
    {
        stateMachine.PhysicUpdate();
    }

    #endregion

    #region PickUp
    private void PickUp()
    {
        pickup = Physics2D.OverlapCircleAll(transform.position, rangePickup, LayerMaskHelper.layerMaskLoot);
        if (pickup.Length == 0)
            return;
        if (pickup.Length > 0)
            foreach (var collider in pickup)
            {
                if (collider.gameObject.TryGetComponent<LootItem>(out LootItem loot))
                    loot.StartCoroutine(loot.MoveCourtine());
            }
    }
    #endregion

    #region save-load
    public void SetPosition(Vector3 cords, Vector2 direction)
    {
        Vector2 moveInput = transform.position;
        Vector2 currentMove = new Vector2(moveInput.x, moveInput.y);
        transform.position = cords;
        currentMove = direction;
    }
    #endregion

}