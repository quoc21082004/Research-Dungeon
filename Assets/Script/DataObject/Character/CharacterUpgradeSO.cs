using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "CharacterSO/CharacterUpgrade")]
public class CharacterUpgradeSO : ScriptableObject
{
    public List<UpgradeCustom> Data;
    public const int MAX_LEVEL = 90;
    public int GetNextLevel(int _level)
    {
        if (_level >= MAX_LEVEL)
            return Data[^1].exp;
        return _level <= 1 ? (int)Data[0].expToLvl : (int)Data[_level - 1].expToLvl;
    }
    public int GetTotalEXP(int _currentLevel) => _currentLevel <= 1 ? 0 :(int)Data[_currentLevel - 2].expToLvl;
}
[System.Serializable] 
public class UpgradeCustom
{
    public int level;
    public int exp;
    public float expToLvl;
}