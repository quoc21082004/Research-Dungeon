using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public abstract class AmtConfirmWindow : MonoBehaviour
{
    public float selectAmt;
    public GameObject amtPanel;
    public GameObject confirmPanel;
    public Button plus_btn, minus_btn;
    public TextMeshProUGUI amt_txt;
    public Button amtConfirm_btn;
    public Button amtCancel_btn;
    public TextMeshProUGUI confirmAction_txt;
    public Slider quantitySlider;
    protected virtual void OnEnable()
    {
        amtPanel.gameObject.SetActive(true);
        confirmPanel.gameObject.SetActive(false);
        amtConfirm_btn.onClick.AddListener(ConfirmAmt);
        amtCancel_btn.onClick.AddListener(CancelAmt);
        quantitySlider.onValueChanged.AddListener(SliderQuantityChange);
    }
    protected virtual void OnDisable()
    {
        amtConfirm_btn.onClick.RemoveListener(ConfirmAmt);
        amtCancel_btn.onClick.RemoveListener(CancelAmt);
        quantitySlider.onValueChanged.RemoveListener(SliderQuantityChange);
        quantitySlider.value = quantitySlider.minValue;
    }
    public abstract void ConfirmAmt();
    public abstract void ConfirmAction();
    protected abstract void SliderQuantityChange(float _value);
    public void CancelAmt()
    {
        AudioManager.instance.PlaySfx("Click");
        gameObject.SetActive(false);
        GetComponentInParent<InventoryUI>().itemOptionsWindow.selectSlotbtn.Select();
        GetComponentInParent<InventoryUI>().itemOptionsWindow.selectSlotbtn.OnSelect(null);
    }
    public void CancelComfirm()
    {
        AudioManager.instance.PlaySfx("Click");
        confirmPanel.gameObject.SetActive(false);
        amtPanel.gameObject.SetActive(true);
    }
    public void InitAmt(int amt)
    {
        selectAmt = amt;
        amt_txt.text = "" + selectAmt;
    }
}
