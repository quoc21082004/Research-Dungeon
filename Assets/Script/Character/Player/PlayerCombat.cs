using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : ActiveAbility 
{
    PlayerController player;
    public static Transform MuzzlePoint;
    public GameObject fireballprefab;
    bool isAttack = false;
    public static GUI_Input mouseFollow;
    public float rangeOfAim;
    Collider2D[] findEnemy;
    public Transform aimPos, muzzleFind;
    private void Awake()
    {
        MuzzlePoint = GameObject.FindGameObjectWithTag("Muzzle").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        mouseFollow = GetComponentInChildren<GUI_Input>();
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    private void Update()
    {
        if (ItemHotKeyManager.instance == null)
            return;
        else
        {
            bool[] hotkeyInputs = new bool[2]
            {
                Input.GetKeyDown(KeyCode.X),
                Input.GetKeyDown(KeyCode.C),
            };
            for (int i = 0; i < ItemHotKeyManager.instance.NumOfHotKeyItem; i++)
                if (hotkeyInputs[i] && !ItemHotKeyManager.instance.IsHotKeyCoolDown(i)) // cool down = false (run)
                    ItemHotKeyManager.instance.UseHotKey(i);
        }

        if (SpellHotKeyManager.instance == null)
            return;
        else
        {
            bool[] hotkeySpell = new bool[4]
            {
                Input.GetKeyDown(KeyCode.Alpha1),
                Input.GetKeyDown(KeyCode.Alpha2),
                Input.GetKeyDown(KeyCode.Alpha3),
                Input.GetKeyDown(KeyCode.Alpha4),
            };
            for (int i = 0; i < SpellHotKeyManager.instance.NumOfHotKeySpell; i++)
                if (hotkeySpell[i] && !SpellHotKeyManager.instance.IsHotKeyCoolDown(i))
                    SpellHotKeyManager.instance.UseHotKey(i);
        }
    }
    private void UseItemX(int index)
    {

    }
    #region hideTag
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
    public void CastFireBall()
    {
        float attackCD = 0.5f;
        if (Input.GetKeyDown(KeyCode.F) && !isAttack)
        {
            isAttack = true;
            GameObject clone = PoolManager.instance.Release(fireballprefab, MuzzlePoint.transform.position, mouseFollow.transform.rotation);
            StartCoroutine(waitCD(attackCD));
        }
    }
    IEnumerator waitCD(float time)
    {
        yield return new WaitForSeconds(time);
        isAttack = false;
    }
    #endregion
}
