﻿using UnityEngine;
using System.Collections.Generic;
using System;
[System.Serializable]
public class Inventory 
{
    public event Action OnItemChangeCallBack;
    public int space = 24;

    public int Gold { get; set; }
    public event Action<int> OnCoinChangedEvent;

    public List<ItemSO> items = new List<ItemSO>();
    public bool AddItem(ItemSO item, int amount)
    {
        if (items.Count >= space)
            return false;
        if (item.isStackable && items.Find(x => x.nameItem.Equals(item.nameItem)) != null)
        {
            ItemSO itemInInventory = items.Find(x => x.nameItem.Equals(item.nameItem)); //             //itemInInventory.currentAmt += item.currentAmt;
            if (itemInInventory != null)
                itemInInventory.currentAmt += amount;
        }
        else 
        {
            items.Add(MonoBehaviour.Instantiate(item));       // add item into list
            items.Sort((x1, x2) => x1.itemNumber.CompareTo(x2.itemNumber)); // sort item number 
        }
        OnItemChangeCallBack?.Invoke();
        return true;
    }
    public void Remove(ItemSO itemSO, int amt)
    {
        /*for (int i = 0; i < items.Count; i++)                         // way 2
        {
            if (itemSO == items[i])
            {
                itemInInventory = items[i];
            }
        }*/
        ItemSO itemInInventory = items.Find(x => x.itemNumber == itemSO.itemNumber); //Equals(itemSO)); // way 1
        itemInInventory.currentAmt -= amt;
        if (itemInInventory.currentAmt <= 0)
        {
            items.Remove(itemInInventory);
            MonoBehaviour.Destroy(itemInInventory);
            items.Sort((x1, x2) => x1.itemNumber.CompareTo(x2.itemNumber));
        }
        OnItemChangeCallBack?.Invoke();
    }
    public void Remove(ItemSO itemSO, bool toDestroy)
    {
        ItemSO itemInventory = items.Find(x => x.Equals(itemSO));
        items.Remove(itemInventory);
        items.Sort((x1, x2) => x1.itemNumber.CompareTo(x2.itemNumber));
        if (toDestroy)
            MonoBehaviour.Destroy(itemInventory);
        OnItemChangeCallBack?.Invoke();
    }
    public void LoadInventory() => OnItemChangeCallBack?.Invoke();
    public int GetItemAmt(ItemSO item)
    {
        if (items.Find(x => x.itemNumber == item.itemNumber) == null)
            return 0;
        else if (items.Find(x => x.itemNumber == item.itemNumber) != null)
            return items.Find(x => x.itemNumber == item.itemNumber).currentAmt;
        return 0;
    }
    public ItemSO GetItem(int itemnumber)
    {
        return items.Find(x => x.itemNumber == itemnumber);
    }
    
    public void IncreaseCoin(int _value)
    {
        Gold = Mathf.Clamp(Gold + _value, 0, Int32.MaxValue);
        OnCoinChangedEvent?.Invoke(_value);
    }
}
