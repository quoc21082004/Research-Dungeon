using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyItemOptions : ItemOptions
{
    public GameObject buyWindow;
    public Button buyButton;

    private void OnEnable()
    {
        buyButton.Select();
        buyButton.OnSelect(null);
    }
    public void OnBuyButton()
    {
        if (InventoryUI.selectedItem == null)
            return;
        AudioManager.instance.PlaySfx("Click");
        if (PartyController.inventoryG.Gold < InventoryUI.selectedItem.buyPrice)
            Debug.Log("u don't have enough gold to buy");
        else if (PartyController.inventoryG.Gold >= InventoryUI.selectedItem.buyPrice) 
        {
            buyWindow.gameObject.SetActive(true);
            buyWindow.gameObject.GetComponent<AmtConfirmWindow>().InitAmt(1);
            gameObject.SetActive(false);
        }
        OnBackButton();
    }
}
