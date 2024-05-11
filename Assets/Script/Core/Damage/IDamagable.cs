using UnityEngine;

public interface IDamagable
{
    void TakeDamage(float amount, bool isCrit);
    float CaculateDMG(float amt, bool isCrit);
}
public interface IKnockBack
{
    void KnockBack(Transform damageSouce, float thurstKnock, Transform parent);
}