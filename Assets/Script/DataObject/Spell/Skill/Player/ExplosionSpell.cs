using UnityEngine;

public class ExplosionSpell : MonoBehaviour, ISpell
{
    private ActiveAbility activeAbility;
    public float explosionRadius;
    float critDamage;
    public Collider2D[] colliders;
    public void KickOff(ActiveAbility ability, Vector2 direction, Quaternion rot)
    {
        activeAbility = ability;
        transform.position = PartyController.player.transform.position;
        transform.parent = PartyController.player.transform;
        Explore();
    }
    private float CaculateDamage()
    {
        var _playerSO = PartyController.player.playerdata.basicAttack;
        critDamage = _playerSO.GetCritDMG();
        float totalDamage = activeAbility.skillInfo.baseDamage + _playerSO.GetDamage();
        return totalDamage;
    }
    public void Explore()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, LayerMaskHelper.layerMaskEnemy);
        if (colliders.Length == 0)
            return;
        foreach(var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<EnemyHurt>(out EnemyHurt enemy))
            {
                bool isCrit = PartyController.player.playerdata.basicAttack.GetCrit() > Random.Range(0f, 101f) ? true : false;
                float totalDMG = isCrit ? CaculateDamage() * 1.35f : CaculateDamage();
                IDamagable damage = collider.gameObject.GetComponent<IDamagable>();
                if (damage != null)
                    damage.TakeDamage(totalDMG, isCrit);
            }
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
    
}
