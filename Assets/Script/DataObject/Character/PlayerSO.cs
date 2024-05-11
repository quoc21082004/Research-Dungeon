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
    public OtherStats otherStats;
    [Space]
    public UpgradeLevelData upgradeLevel;
}

#region Stats Full
[System.Serializable] 
public class BasicMovement
{
    public float baseSpeed;
    public float runSpeed;
    public float dashSpeed;
}
[System.Serializable]
public class BasicAttack
{
    public float wandDamage;
    public float critChance;
    public float minCritDamage;
    public float maxCritDamage;
    public float builetSpeed;
    public float attackSpeed;
    public float percentDamage;
}
[System.Serializable]
public class OtherStats
{
    public float damageReduction;
    public int skillPoint;
    public int gold;
}
[System.Serializable]
public class BasicStats
{
    public float health;
    public float mana;
    public float defense;
    public float healthRegen;
    public float manaRegen;
}
[System.Serializable]
public class UpgradeLevelData
{
    public int level;
    public float exp;
    public float expToLvl;
}
#endregion
