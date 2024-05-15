using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "CharacterSO/EquipmentUpgrade")] 
public class EquipmentUpgradeSO : ScriptableObject
{
    public int MAX_LEVEL = 10;
    public List<RequireData> requireData;
    public RequireData GetRequireData(int _level) => requireData[_level - 1];
}
[System.Serializable]
public class RequireData
{
    public int cost;
    public List<UpgradeItem> requireItems;
}
[System.Serializable] 
public class UpgradeItem
{
    public ItemSO requireItem;
    public int value;
}
