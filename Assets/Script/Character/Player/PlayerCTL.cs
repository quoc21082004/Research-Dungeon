﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCTL : MonoBehaviour
{
    public const string FILE_NAME = "PlayerStatus";

    #region Info Player
    [HideInInspector] public SpriteRenderer mySR;
    [HideInInspector] public Rigidbody2D myrigid;
    Animator myanim;
    TrailRenderer mytrail;
    [HideInInspector] public PlayerHurt playerhurt;
    [HideInInspector] public PlayerCombat playercombat;
    [HideInInspector] public float maxhealth, maxmana, health, damage, defense, mana, speed;
    [HideInInspector] public bool isAlve = true;
    [HideInInspector] public PlayerSO playerdata;

    public float dashSpeed, dashCD;
    bool isDash;
    Collider2D[] pickup;
    [HideInInspector] public float rangePickup = 1.2f;
    public MouseFollow mousefollow;
    #endregion

    private PlayerMovementStateMachine movementState; 
    private void Awake()
    {
        movementState = new PlayerMovementStateMachine(this);
        mySR = GetComponent<SpriteRenderer>();
        myrigid = GetComponent<Rigidbody2D>();
        myanim = GetComponent<Animator>();
        playerhurt = GetComponentInParent<PlayerHurt>();
        playercombat = GetComponent<PlayerCombat>();
        mytrail = GetComponentInChildren<TrailRenderer>();
        mousefollow = GetComponentInChildren<MouseFollow>();
    }
    private void OnEnable()
    {
        playerdata = GameManager.instance.playerSO;
        health = playerdata.basicStats.health;
        mana = playerdata.basicStats.mana;
    }
    private void Start()
    {
        movementState.ChangeState(movementState.IdleState);
        LoadPos();
    }
    private void OnApplicationQuit()
    {
        SavePos();
    }
    private void Update()
    {
        movementState.HandleInput();
        movementState.Update();

        maxhealth = playerdata.basicStats.health;
        maxmana = playerdata.basicStats.mana;
        defense = playerdata.basicStats.defense;
        damage = playerdata.basicAttack.wandDamage;
        speed = playerdata.basicMovement.baseSpeed;
        if (isAlve)
        {
            PickUp();
            Dash();
            playerhurt.RegenRecover();
            playercombat.FindEnemy();
        }
    }
    private void FixedUpdate()
    {
        movementState.PhysicUpdate();
        /*if (health >= playerdata.basicStats.health)
            health = playerdata.basicStats.health;
        if (mana >= playerdata.basicStats.mana)
            mana = playerdata.basicStats.mana;*/
    }

    #region move & flip
    private void Move()
    {
        /*moveInput = InputManager.playerInput.Player.Move.ReadValue<Vector2>();
        if ((moveInput.x != 0 | moveInput.y != 0)) 
        {
            myanim.SetFloat("MoveX", moveInput.x);
            myanim.SetFloat("MoveY", moveInput.y);
            myanim.SetBool("Walking", true);
            myrigid.velocity = (moveInput * speed * Time.deltaTime);
        }
        else if (moveInput.x == 0 && moveInput.y == 0) 
        {
            myanim.SetBool("Walking", false);
            myrigid.velocity = Vector2.zero;
        }*/
    }
    public void SetPosition(Vector3 cords, Vector2 direction)
    {
        Vector2 moveInput = transform.position;
        Vector2 currentMove = new Vector2(moveInput.x, moveInput.y);
        transform.position = cords;
        currentMove = direction;
    }
    #endregion

    #region Dash
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDash)
        {
            isDash = true;
            speed *= dashSpeed;
            mytrail.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }
    private IEnumerator EndDashRoutine()
    {
        float dashTime = 0.2f;
        yield return new WaitForSeconds(dashTime);
        speed = speed / 2;
        yield return new WaitForSeconds(dashCD);
        mytrail.emitting = false;
        isDash = false;
    }
    #endregion

    #region PickUp
    void PickUp()
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
    public void SavePos()
    {
        Vector3 tempPos = transform.position;
        SaveLoadHandler.SaveToJson<Vector3>(tempPos, FILE_NAME);
    }
    public void LoadPos()
    {
        Vector3 afterPos = SaveLoadHandler.LoadFromFile<Vector3>(FILE_NAME);
        transform.position = afterPos;
    }
    #endregion
}

