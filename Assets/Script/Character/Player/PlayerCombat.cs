using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : ActiveAbility 
{
    PlayerCTL player;
    public static Transform muzzlePoint;
    public GameObject fireballprefab;
    bool isAttack = false;
    MouseFollow mouseFollow;
    public float rangeOfAim;
    Collider2D[] findEnemy;
    public Transform aimPos, muzzleFind;
    private void OnEnable()
    {
        muzzlePoint = GameObject.FindGameObjectWithTag("Muzzle").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<PlayerCTL>();
        mouseFollow = GetComponentInChildren<MouseFollow>();
        RegisterEvent();
    }
    private void OnDisable()
    {
        UnRegisterEvent();
    }
    private void RegisterEvent()
    {
        var abilityInput = InputManager.playerInput.PlayerAbility;
        abilityInput.Consume1.performed += ConsumeItemX;
        abilityInput.Consume2.performed += ConsumeItemC;
        abilityInput.Ability0.performed += UseAbility0;
        abilityInput.Ability1.performed += UseAbility1;
        abilityInput.Ability2.performed += UseAbility2;
        abilityInput.Ability3.performed += UseAbility3;
        abilityInput.Ability4.performed += UseAbility4;
    }
    private void UnRegisterEvent()
    {
        var abilityInput = InputManager.playerInput.PlayerAbility;
        abilityInput.Consume1.performed -= ConsumeItemX;
        abilityInput.Consume2.performed -= ConsumeItemC;
        abilityInput.Ability0.performed -= UseAbility0;
        abilityInput.Ability1.performed -= UseAbility1;
        abilityInput.Ability2.performed -= UseAbility2;
        abilityInput.Ability3.performed -= UseAbility3;
        abilityInput.Ability4.performed -= UseAbility4;
    }
    private void ConsumeItemX(InputAction.CallbackContext inputAction)
    {
        if (ItemHotKeyManager.instance == null)
            return;
        if (!ItemHotKeyManager.instance.IsHotKeyCoolDown(0)) // cool down = false (run)
            ItemHotKeyManager.instance.UseHotKey(0);
    }
    private void ConsumeItemC(InputAction.CallbackContext inputAction)
    {
        if (ItemHotKeyManager.instance == null)
            return;
        if (!ItemHotKeyManager.instance.IsHotKeyCoolDown(1)) // cool down = false (run)
            ItemHotKeyManager.instance.UseHotKey(1);
    }
    private void UseAbility0(InputAction.CallbackContext inputAction)
    {
        float attackCD = 0.5f;
        if (!isAttack) 
        {
            isAttack = true;
            PoolManager.instance.Release(fireballprefab, muzzlePoint.transform.position, mouseFollow.transform.rotation);
            StartCoroutine(waitCD(attackCD));
        }
    }
    private void UseAbility1(InputAction.CallbackContext inputAction)
    {
        if (SpellHotKeyManager.instance == null)
            return;
        if (!SpellHotKeyManager.instance.IsHotKeyCoolDown(0))
            SpellHotKeyManager.instance.UseHotKey(0);
    }
    private void UseAbility2(InputAction.CallbackContext inputAction)
    {
        if (SpellHotKeyManager.instance == null)
            return;
        if (!SpellHotKeyManager.instance.IsHotKeyCoolDown(1))
            SpellHotKeyManager.instance.UseHotKey(1);
    }
    private void UseAbility3(InputAction.CallbackContext inputAction)
    {
        if (SpellHotKeyManager.instance == null)
            return;
        if (!SpellHotKeyManager.instance.IsHotKeyCoolDown(2))
            SpellHotKeyManager.instance.UseHotKey(2);
    }
    private void UseAbility4(InputAction.CallbackContext inputAction)
    {
        if (SpellHotKeyManager.instance == null)
            return;
        if (!SpellHotKeyManager.instance.IsHotKeyCoolDown(3))
            SpellHotKeyManager.instance.UseHotKey(3);
    }

    #region LF Enemy
    public void FindEnemy()
    {
        findEnemy = Physics2D.OverlapCircleAll(transform.position, rangeOfAim, LayerMaskHelper.layerMaskEnemy);
        if (findEnemy.Length == 0)
        {
            muzzleFind.transform.rotation = Quaternion.AngleAxis(0, Vector3.right);
            return;
        }
        Collider2D randomEnemy = findEnemy[Random.Range(0, findEnemy.Length)];
        if (randomEnemy.TryGetComponent<Enemy>(out Enemy enemy))
        {
            float angle = 0;
            Vector3 dir = enemy.transform.position - transform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            muzzleFind.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    IEnumerator waitCD(float time)
    {
        yield return new WaitForSeconds(time);
        isAttack = false;
    }
    #endregion
}
