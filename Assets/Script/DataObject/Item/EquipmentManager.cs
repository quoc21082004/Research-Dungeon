using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EquipmentManager : Singleton<EquipmentManager>
{
    public Equipment[] currentEquipment;
    int numOfEquipment;

    private void Start()
    {
        numOfEquipment = Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numOfEquipment];
    }
    public void Equip(Equipment newEquipment)
    {
        int slotEquipIndex = (int)newEquipment.equipSlot;
        var oldItem = Instantiate(newEquipment);
        if (currentEquipment[slotEquipIndex] != null)
        {

        }
        else
        {
            currentEquipment[slotEquipIndex] = oldItem;
            AddModifier(currentEquipment[slotEquipIndex]);
            PartyController.inventoryG.Remove(newEquipment, 1);
        }
    }
    public void UnEquip(int _slotindex)
    {
        if (currentEquipment[_slotindex] != null)
        {
            Equipment oldItem = currentEquipment[_slotindex];
            PartyController.inventoryG.AddItem(oldItem, 1);
            RemoveModifier(currentEquipment[_slotindex]);
            currentEquipment[_slotindex] = null;
        }
    }
    public void AddModifier(Equipment modifier)
    {
        var playerSO = GameManager.instance.playerSO;
        playerSO.basicStats.defense = Mathf.Max(0, playerSO.basicStats.defense + modifier.armorModifier);
        playerSO.basicAttack.wandDamage = Mathf.Max(0, playerSO.basicAttack.wandDamage + modifier.atkModifier);
    }
    public void RemoveModifier(Equipment modifier)
    {
        var playerSO = GameManager.instance.playerSO;
        playerSO.basicStats.defense = Mathf.Max(0, playerSO.basicStats.defense - modifier.armorModifier);
        playerSO.basicAttack.wandDamage = Mathf.Max(0, playerSO.basicAttack.wandDamage - modifier.atkModifier);
    }
}