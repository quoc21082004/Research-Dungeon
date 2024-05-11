using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu]
public class EnemySO: ScriptableObject 
{
    [Header("Stats Modifier")]
    [SerializeField] private int HP;
    [SerializeField] private int DEF;
    [SerializeField] private int defense;
    [SerializeField] private float attackTimer;
    [SerializeField] private float alertRange;
    [SerializeField] private float range;
    public float builetSpeed;
    [Space]
    [SerializeField] public TypeEnemy Type;
    [SerializeField] public bool isDead;
    [SerializeField] public bool isAlert;
    [SerializeField] public bool isAttack;
    [Space]
    [SerializeField] public int goldReward;
    [SerializeField] public int expReward;
    [Space]
    public GrowStat growstats;

    // Function
    public int GetHP() => HP;
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