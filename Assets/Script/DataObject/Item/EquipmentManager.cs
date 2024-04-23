using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Newtonsoft.Json;
public class EquipmentManager : Singleton<EquipmentManager>
{
    public const string FILE_NAME = "Equipment.json";
    public Equipment[] currentEquipment;
    int numOfEquipment;
    public event Action OnEquipItem;
    private void Start()
    {
        numOfEquipment = Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numOfEquipment];
    }
    private void OnApplicationQuit()
    {

    }

    #region Equip - UnEquip
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
            OnEquipItem?.Invoke();
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
            OnEquipItem?.Invoke();
        }
    }
    #endregion

    #region Add Stats - Remove Stats
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
    #endregion
}