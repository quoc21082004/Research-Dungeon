using TMPro;
using UnityEngine;

[CreateAssetMenu]
public class EnemySO: ScriptableObject 
{
    [Header("Stats Modifier")]
    [SerializeField] private int HP;
    [SerializeField] private int Def;
    [SerializeField] private int Damage;
    [SerializeField] private float attackTimer;
    [SerializeField] private float alertRange;
    [SerializeField] private float range;
    [SerializeField] private float builetSpeed;
    [Space]
    [SerializeField] public TypeEnemy Type;
    [Space]
    [SerializeField] private int goldReward;
    [SerializeField] private int expReward;
    [Space]
    [SerializeField] private GrowStat growstats;

    // Function
    public int GetHP() => HP;
    public void SetHP(int _value) => HP = _value;

    public int GetDef() => Def;
    public void SetDef(int _value) => Def = _value;

    public int GetDamage() => Damage;
    public void SetDmg(int _value) => Damage = _value;

    public float GetAttackTimer() => attackTimer;
    public void SetHP(float _value) => attackTimer = _value;

    public float GetAlertRange() => alertRange;
    public void SetAlertRange(int _value) => alertRange = _value;
    public float GetRange() => range;
    public void SetRange(float _value) => range = _value;
    public float GetBuiletSpeed() => builetSpeed;
    public void SetBuiletSpeed(float _value) => builetSpeed = _value;

    public int GetGoldReward() => goldReward;
    public void SetGoldReward(int _value) => goldReward = _value;

    public int GetExpReward() => expReward;
    public void SetExpReward(int _value) => expReward = _value;

    public GrowStat GetGrowStat() => growstats;

}
[System.Serializable]
public class GrowStat
{
    [SerializeField] private int healthGrow;
    [SerializeField] private int damageGrow;
    [SerializeField] private int defenseGrow;

    public int GetHealthGrown() => healthGrow;
    public int GetDamageGrown() => damageGrow;
    public int GetDefenseGrown() => defenseGrow;
}