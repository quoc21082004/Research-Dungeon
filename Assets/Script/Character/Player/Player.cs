using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    SpriteRenderer mySR;
    Rigidbody2D myrigid;
    Animator myanim;
    Collider2D mycollider;
    TrailRenderer mytrail;
    [HideInInspector] public PlayerHurt playerhurt;
    [HideInInspector] public PlayerCombat playercombat;
    [HideInInspector] public float maxhealth, maxmana, health, damage, defense, mana, speed;
    [HideInInspector] public bool isAlve = true;
    public PlayerSO playerdata;
    float startSpeed;
    public static bool isFace, canAction;
    public float dashSpeed, dashCD;
    bool isDash;
    Collider2D[] pickup;
    [HideInInspector] public float rangePickup = 1.2f;
    Vector2 moveInput;
    public event Action OnDieEvent;
    private void Awake()
    {
        startSpeed = speed;
        mySR = GetComponent<SpriteRenderer>();
        myrigid = GetComponent<Rigidbody2D>();
        mycollider = GetComponent<Collider2D>();
        myanim = GetComponent<Animator>();
        playerhurt = GetComponentInParent<PlayerHurt>();
        playercombat = GetComponent<PlayerCombat>();
        mytrail = GetComponentInChildren<TrailRenderer>();
    }
    private void Start()
    {
        canAction = true;
        health = playerdata.basicStats.health;
        mana = playerdata.basicStats.mana;
    }
    private void Update()
    {
        maxhealth = playerdata.basicStats.health;
        maxmana = playerdata.basicStats.mana;
        defense = playerdata.basicStats.defense;
        damage = playerdata.basicAttack.wandDamage;
        speed = PlayerPrefs.GetFloat("speed") + (PlayerPrefs.GetFloat("speed") * PlayerPrefs.GetFloat("bounsSpeed")) / 100;
        if (isAlve && canAction)
        {
            PickUp();
            Dash();
            playerhurt.RegenRecover();
            playercombat.FindEnemy();
        }
    }
    private void FixedUpdate()
    {
        Move();
        FlipCharacter();
        if (health >= playerdata.basicStats.health)
            health = playerdata.basicStats.health;
        if (mana >= playerdata.basicStats.mana)
            mana = playerdata.basicStats.mana;
    }
    #region move & flip
    private void Move()
    {
        moveInput = InputManager.playerInput.Player.Move.ReadValue<Vector2>();
        if ((moveInput.x != 0 | moveInput.y != 0) && canAction) 
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
        }
    }
    private void FlipCharacter()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenShot = Camera.main.WorldToScreenPoint(transform.position);
        if (mousePos.x < playerScreenShot.x)
        {
            mySR.flipX = true;
            isFace = true;
        }
        else
        {
            mySR.flipX = false;
            isFace = false;
        }

    }
    #endregion
    public void SetPosition(Vector3 cords, Vector2 direction)
    {
        Vector2 currentMove = new Vector2(moveInput.x, moveInput.y);
        this.transform.position = cords;
        currentMove = direction;
    }
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
        speed = startSpeed;
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
}

