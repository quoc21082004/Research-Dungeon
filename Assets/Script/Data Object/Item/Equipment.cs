using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Item", menuName = "Shop/Equipment")] 
public class Equipment : Consumable
{
    public EquipmentSlot equipSlot;
    public int armorModifier;
    public int damageModifier;
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
    }
}
public enum EquipmentSlot       // 0 - 3
{
    Head,   
    Chest,
    Legs,
    Feet,
}