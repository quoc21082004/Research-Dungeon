using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DiscardWindow : AmtConfirmWindow
{
    protected override void OnEnable()
    {
        base.OnEnable();
        plus_btn.onClick.AddListener(PlusButton);
        minus_btn.onClick.AddListener(MinusButton);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        plus_btn.onClick.RemoveListener(PlusButton);
        minus_btn.onClick.RemoveListener(MinusButton);
    }
    private void PlusButton()
    {
        if ((int)selectAmt < InventoryUI.selectedItem.currentAmt) 
        {
            AudioManager.instance.PlaySfx("Click");
            selectAmt++;
            amt_txt.text = "" + (int)selectAmt;
            SliderQuantityChange(selectAmt);
        }
    }
    private void MinusButton()
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
        confirmAction_txt.text = string.Format("Discarding\n"
                                + "{0} x{1} \n"
                                + "Confirm ?", InventoryUI.selectedItem.name, (int)selectAmt);
    }
    public override void ConfirmAction()
    {
        InventoryUI.selectedItem.RemoveFromInventory((int)selectAmt);
        AudioManager.instance.PlaySfx("Purchase");
        this.gameObject.SetActive(false);
    }
}
