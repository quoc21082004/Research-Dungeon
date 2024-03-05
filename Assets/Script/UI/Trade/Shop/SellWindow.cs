using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class SellWindow : AmtConfirmWindow
{
    protected override void OnEnable()
    {
        base.OnEnable();
        plus_btn.onClick.AddListener(PlusBtn);
        minus_btn.onClick.AddListener(MinusBtn);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        plus_btn.onClick.RemoveListener(PlusBtn);
        minus_btn.onClick.RemoveListener(MinusBtn);
    }
    void PlusBtn()
    {
        if (selectAmt < InventoryUI.selectedItem.currentAmt)
        {
            AudioManager.instance.PlaySfx("Click");
            selectAmt++;
            amt_txt.text = "" + (int)selectAmt;
            SliderQuantityChange(selectAmt);
        }
    }
    void MinusBtn()
    {
        if (selectAmt > 1)
        {
            AudioManager.instance.PlaySfx("Click");
            selectAmt--;
            amt_txt.text = "" + (int)selectAmt;
            SliderQuantityChange(selectAmt);
        }
    }
    protected override void SliderQuantityChange(float _value)
    {
        selectAmt = _value;
        quantitySlider.maxValue = InventoryUI.selectedItem.currentAmt;
        quantitySlider.value = selectAmt;
        amt_txt.text = "" + (int)selectAmt;
    }
    public override void ConfirmAmt()
    {
        AudioManager.instance.PlaySfx("Click");
        amtPanel.gameObject.SetActive(false);
        confirmPanel.gameObject.SetActive(true);
        confirmAction_txt.text = string.Format("Selling\n"
                                             + "{0} x{1} \n"
                                             + "for \n"
                                             + "{2}" + "  <sprite=3> \n"
                                             + "Confirm ?",
                                             InventoryUI.selectedItem.name, (int)selectAmt, InventoryUI.selectedItem.sellPrice * (int)selectAmt);
    }
    public override void ConfirmAction()
    {
        InventoryUI.selectedItem.SellForGold((int)selectAmt);
        AudioManager.instance.PlaySfx("Purchase");
        gameObject.SetActive(false);
        confirmPanel.gameObject.SetActive(false);
        amtPanel.gameObject.SetActive(true);
    }
}
