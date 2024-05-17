using System.Collections;
using UnityEngine;

public class PosionZone : MonoBehaviour, ISpell
{
    Animator myanim;
    private ActiveAbility abilitySpell;
    Collider2D[] colliders;
    public float prepareTime;
    public float durationTime;
    public float damageTimeEclipse;
    public float damageRange;
    float critDamage;
    private void Awake()
    {
        myanim = GetComponent<Animator>();
    }
    public void KickOff(ActiveAbility ability, Vector2 direction, Quaternion rot)
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos) ;
        abilitySpell = ability;
        direction = Vector2.ClampMagnitude(direction, abilitySpell.MaxUseRange);
        transform.position = (mousePos - direction);
        StartCoroutine(StartPrepare());
    }
    private float CaculateDamage()
    {
        var _playerSO = PartyController.player.playerdata.basicAttack;
        critDamage = _playerSO.GetCritDMG();
        float ratePercent = Random.Range(150f, 170f);
        float totalDamage = (((abilitySpell.skillInfo.baseDamage + _playerSO.GetDamage() * ratePercent) / 100)) * Random.Range(1.75f, 2.5f);
        return totalDamage;
    }
    private IEnumerator StartPrepare()
    {
        yield return new WaitForSeconds(prepareTime);
        myanim.Play("PoisonZoneDamage");
        var endTime = Time.time + durationTime;
        while (Time.time <= endTime) 
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, damageRange, LayerMaskHelper.layerMaskEnemy);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<EnemyHurt>(out var enemy))
                {
                    bool isCrit = PartyController.player.playerdata.basicAttack.GetCrit() > Random.Range(0, 101) ? true : false;
                    float totalDMG = isCrit ? CaculateDamage() * critDamage : CaculateDamage();
                    IDamagable damage = collider.gameObject.GetComponent<IDamagable>();
                    if (damage != null)
                        damage.TakeDamage(totalDMG, isCrit);
                }
            }
            yield return new WaitForSeconds(damageTimeEclipse);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position, damageRange);
    }
}
