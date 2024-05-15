using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO",menuName = "CharacterSO/Character")]
public class PlayerSO : ScriptableObject 
{
    public int MAX_LEVEL = 99;

    public BasicStats basicStats;
    [Space]
    public BasicMovement basicMovement;
    [Space]
    public BasicAttack basicAttack;
    [Space]
    public UpgradeLevelData upgradeLevel;
}

#region Stats Full
[System.Serializable] 
public class BasicMovement
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float dashSpeed;

    public float GetBaseSpeed() => baseSpeed;
    public void SetBaseSpeed(float _value) => baseSpeed = _value;
    public float GetRunSpeed() => runSpeed;
    public void SetRunSpeed(float _value) => runSpeed = _value;
    public float GetDashSpeed() => dashSpeed;
    public void SetDashSpeed(float _value) => dashSpeed = _value;
}
[System.Serializable]
public class BasicAttack
{
    [SerializeField] private int wandDamage;
    [SerializeField] private float critChance;
    [SerializeField] private float critDamage;
    [SerializeField] private float builetSpeed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float percentDamage;

    public int GetDamage() => wandDamage;
    public void SetDamage(int _value) => wandDamage = _value;
    public float GetCrit() => critChance;
    public void SetCrit(float _value) => critChance = _value;
    public float GetCritDMG() => critDamage;
    public void SetCritDMG(float _value) => critDamage = _value;
    public float GetBuiletSpeed() => builetSpeed;
    public void SetBuiletSpeed(float _value) => builetSpeed = _value;
    public float GetAttackSpeed() => attackSpeed;
    public void SetAttackSpeed(float _value) => attackSpeed = _value;
    public float GetPercentDMG() => percentDamage;
    public void SetPercentDMG(float _value) => percentDamage = _value;
}
[System.Serializable]
public class OtherStats
{
    //public float damageReduction;
    //public int gold;
}
[System.Serializable]
public class BasicStats
{
    [SerializeField] private int health;
    [SerializeField] private int mana;
    [SerializeField] private int defense;
    [SerializeField] private float healthRegen;
    [SerializeField] private float manaRegen;
    [SerializeField] private float damageReduce;
    public int GetHealth() => health;
    public void SetHealth(int _value) => health = _value;
    public int GetMana() => mana;
    public void SetMana(int _value) => mana = _value;
    public int GetDef() => defense;
    public void SetDef(int _value) => defense = _value;
    public float GetHealthRegen() => healthRegen;
    public void SetHealthRegen(int _value) => healthRegen = _value;
    public float GetManaRegen() => manaRegen;
    public void SetManaRegen(int _value) => manaRegen = _value;

    public float GetDamageReduce() => damageReduce;
    public void SetDamageReduce(float _value) => damageReduce = _value;

}
[System.Serializable]
public class UpgradeLevelData
{
    [SerializeField] private int level;
    [SerializeField] private int exp;
    [SerializeField] private int expToLvl;
    [SerializeField] private int gold;
    public int GetLevel() => level;
    public void SetLevel(int _value) => level = _value;
    public int GetExp() => exp;
    public void SetExp(int _value) => exp = _value;
    public int GetExpToLevel() => expToLvl;
    public void SetExpToLevel(int _value) => expToLvl = _value;
    public int GetGold() => gold;
    public void SetGold(int _value) => gold = _value;
}
#endregion
