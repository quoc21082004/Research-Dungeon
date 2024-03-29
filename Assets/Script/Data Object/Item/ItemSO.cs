using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Shop Item", menuName = "Shop/Mobs Item")]
public class ItemSO : ScriptableObject
{
    public string nameItem;
    public Sprite icon = null;
    public int itemNumber;
    public int currentAmt = 1;
    public bool isStackable;
    [TextArea(0, 2)] public string itemDescription;
    [TextArea(0, 2)] public string flavor;
    public int buyPrice;
    public int sellPrice;
    public ItemRarity Rarity;
    public ItemType Type;
    public virtual void Use()
    {

    }
    public int GetAmtInInventory()
    {
        return PartyController.inventoryG.GetItemAmt(this);
    }
    public void RemoveFromInventory(int amt) => PartyController.inventoryG.Remove(this, amt);
    public void BoughtForGold(int selectAmt)
    {
        PartyController.inventoryG.Gold -= selectAmt * buyPrice;
        ItemSO clone = Instantiate(this);
        clone.currentAmt = selectAmt;
        PartyController.inventoryG.AddItem(clone, clone.currentAmt);
    }
    public void SellForGold(int selectamt)
    {
        PartyController.inventoryG.Gold += selectamt * sellPrice;
        PartyController.inventoryG.Remove(this, selectamt);
    }
    public void LearnForGold(int learncost)
    {
        PartyController.IncreaseCoin(-learncost);
        PartyController.inventoryG.LoadInventory();
    }

}
