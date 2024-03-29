using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopInventoryUI : InventoryUI
{
    public List<ItemSO> shopItemList;
    protected override void Awake()
    {
        inventory = PartyController.inventoryG;
        //slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>().ToList();
        for (int i = 0; i < shopItemList.Count + 2; i++) 
        {
            var spawnSlot = PoolManager.instance.Release(slotprefab.gameObject);
            spawnSlot.transform.SetParent(itemsParent);
            spawnSlot.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        inventory.onItemChangedCallBack += UpdateUI;
    }
    protected override void UpdateUI()
    {
        for (int i = 0; i < slots.Count; i++) 
        {
            if (i < shopItemList.Count)
            {
                shopItemList.Sort((x1, x2) => x1.buyPrice.CompareTo(x2.buyPrice)); // sort price 
                slots[i].AddItem(shopItemList[i], i);
            }
            else
                slots[i].ClearSlot();
        }
        if (selectedItem != null && selectedItem.currentAmt < 0)
            selectedItem = null;
        selectItemDisplay.UpdateUI();
        gold_text.text = "" + inventory.Gold;
    }
}
