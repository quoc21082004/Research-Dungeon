using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : InventorySlot
{
    public int level;
    public TextMeshProUGUI equipInfo;

    public override void SelectItem()
    {
        base.SelectItem();
    }
}
