using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipItemOptions : ItemOptions
{
    public Button unEquip_btn;
    int equipmentIndex;
    private void OnEnable()
    {
        unEquip_btn.Select();
        unEquip_btn.OnSelect(null);
    }
    private void Update()
    {
        if (selectSlotbtn != null)
        {
            equipmentIndex = selectSlotbtn.GetComponentInParent<EquipSlot>().GetItemValue;
        }
    }
    public void OnUnEquipButton()
    {
        EquipmentManager.instance.UnEquip(equipmentIndex);
        OnBackButton();
        gameObject.SetActive(false);
    }
}
