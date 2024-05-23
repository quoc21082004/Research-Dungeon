using UnityEngine;
public class PlayerBuilet : MonoBehaviour
{
    private Rigidbody2D myrigd;
    public AttackSO baseAttack;
    [SerializeField] GameObject damageBurstFX;
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
        transform.Translate(new Vector2(speed, 0f) * Time.deltaTime, Space.Self);
        //  way 2 : myrigd.velocity = Vector3.ClampMagnitude(transform.right * speed, speed );  // transform.right * speed;
    }
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
    #endregion
}