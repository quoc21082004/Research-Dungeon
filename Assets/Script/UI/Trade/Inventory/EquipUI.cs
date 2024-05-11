using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
public class EquipUI : InventoryUI
{
    int numOfEquipment;
    List<Equipment> equipmentTemp;
    protected override void Awake()
    {
        numOfEquipment = Enum.GetNames(typeof(EquipmentSlot)).Length;
        for (int i = 0; i < numOfEquipment; i++) // head- chest - leg - feet
        {
            var spawnSlot = PoolManager.instance.Release(slotprefab.gameObject);
            spawnSlot.transform.SetParent(itemsParent);
            spawnSlot.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        slots = itemsParent.GetComponentsInChildren<InventorySlot>().ToList();
        inventory.OnItemChangeCallBack += UpdateUI;
    }
    protected override void OnEnable() => EquipmentManager.instance.OnEquipItem += Equip;
    protected override void UpdateUI()
    {
        if (selectedItem != null && selectedItem.currentAmt <= 0)
            selectedItem = null;
        selectItemDisplay.UpdateUI();
        gold_text.text = "" + inventory.Gold;
    }
    public void Equip()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (EquipmentManager.instance.currentEquipment[i] != null)
            {
                slots[i].AddItem(EquipmentManager.instance.currentEquipment[i], i);
                slots[i].stackItem_text.enabled = false;
                slots[i].GetComponent<EquipSlot>().GetItemValue = i; 
            }
            else if (EquipmentManager.instance.currentEquipment[i] == null)       // unequip
                slots[i].ClearSlot();
        }
    }
}