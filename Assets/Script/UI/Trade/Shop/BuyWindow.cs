using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Security.Cryptography;
public class BuyWindow : AmtConfirmWindow
{
    protected override void OnEnable()
    {
        base.OnEnable();
        plus_btn.onClick.AddListener(Plusbtn);
        minus_btn.onClick.AddListener(Minusbtn);
        minus_btn.interactable = selectAmt > 0;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        plus_btn.onClick.RemoveListener(Plusbtn);
        minus_btn.onClick.RemoveListener(Minusbtn);
    }
    protected override void SliderQuantityChange(float _value)
    {
        selectAmt = _value;
        quantitySlider.maxValue = (int)(PartyController.inventoryG.Gold / InventoryUI.selectedItem.buyPrice);
        quantitySlider.value = selectAmt;
        amt_txt.text = "" + (int)selectAmt;
    }
    void Plusbtn()
    {
        if ((int)(selectAmt + 1) * InventoryUI.selectedItem.buyPrice <= PartyController.inventoryG.Gold)  
        {
            AudioManager.instance.PlaySfx("Click");
            selectAmt++;
            minus_btn.interactable = selectAmt > 0;
            amt_txt.text = "" + (int)selectAmt;
            SliderQuantityChange(selectAmt);
        }
    }
    void Minusbtn()
    {
        if (selectAmt > 1)
        {
            AudioManager.instance.PlaySfx("Click");
            selectAmt--;
            //quantitySlider.value = selectAmt;
            amt_txt.text = "" + (int)selectAmt;
            SliderQuantityChange(selectAmt);
        }
    }
    public override void ConfirmAmt()
    {
        AudioManager.instance.PlaySfx("Click");
        amtPanel.gameObject.SetActive(false);
        confirmPanel.gameObject.SetActive(true);
        confirmAction_txt.text = string.Format("Buying \n"
                            + "{0} x{1} \n"
                            + "for\n"
                            + "{2}" + "  <sprite=3>" + "\n"
                            + "Confirm ?",
                            InventoryUI.selectedItem.name, (int)selectAmt, InventoryUI.selectedItem.buyPrice * (int)selectAmt);
    }
    public override void ConfirmAction()
    {
        InventoryUI.selectedItem.BoughtForGold((int)selectAmt);
        AudioManager.instance.PlaySfx("Purchase");
        gameObject.SetActive(false);
    }
}
