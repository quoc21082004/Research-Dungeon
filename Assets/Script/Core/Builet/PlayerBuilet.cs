using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerBuilet : MonoBehaviour
{
    private Rigidbody2D myrigd;
    public AttackSO baseAttack;
    [SerializeField] GameObject damageBurstFX;
    [SerializeField] public float realspeed;
    private float critRate, critdamage, percentageDamage, wandDamage, rand, speed;

    #region Main Method
    private void Awake() => myrigd = GetComponent<Rigidbody2D>();
    private void OnEnable()
    {
        var _playerSO = PartyController.player.playerdata;
        speed = _playerSO.basicAttack.GetBuiletSpeed();
        rand = Random.Range(0f, 101f);
        critRate = _playerSO.basicAttack.GetCrit();
        critdamage = _playerSO.basicAttack.GetCritDMG();
        percentageDamage = _playerSO.basicAttack.GetPercentDMG();
        wandDamage = _playerSO.basicAttack.GetDamage() + baseAttack.baseDamage;
    }
    private void FixedUpdate()
    {
        myrigd.AddForce(transform.right * speed * realspeed, ForceMode2D.Impulse);
        myrigd.velocity = Vector2.ClampMagnitude(myrigd.velocity, speed * realspeed);
    }
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyHurt>(out var cos))
        {
            float total = rand < critRate ? wandDamage * critdamage + (wandDamage * percentageDamage) / 100 : wandDamage + (wandDamage * percentageDamage) / 100;
            IDamagable damage = collision.gameObject.GetComponent<IDamagable>();
            if (damage != null)
                damage.TakeDamage(total, rand < critRate);
            AssetManager.instance.assetData.SpawnBloodSfx(collision.transform);
            PoolManager.instance.Release(damageBurstFX, collision.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Wall") && !collision.gameObject.CompareTag("Enemy"))
        {
            PoolManager.instance.Release(damageBurstFX, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}