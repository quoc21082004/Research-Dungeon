using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GUI_Shop : MonoBehaviour , IGUI
{
    [SerializeField] GameObject ShopMenuUI;
    [SerializeField] GameObject BuyUI;
    [SerializeField] GameObject SellUI;

    [SerializeField] Button buybtn, sellbtn;
     bool isShopOpen = false;
    private void OnEnable()
    {
        var guiInput = GUI_Input.playerInput.UI;
        guiInput.OpenShop.performed += OpenShop;
    }
    private void OnDisable()
    {
        var guiInput = GUI_Input.playerInput.UI;
        guiInput.OpenShop.performed -= OpenShop;
    }
    public void GetReference(GameManager _gameManager) { }
    public void UpdateDataGUI() { }
    private void OpenShop(InputAction.CallbackContext context)
    {
        if (!isShopOpen)
            Shop();
        else if (isShopOpen)
            CloseShop();
    }
    public void Shop()
    {
        isShopOpen = true;
        ShopMenuUI.SetActive(true);
        GUI_Input.playerInput.UI.OpenMap.Disable();
        InputManager.playerInput.Disable();
    }
    public void CloseShop()
    {
        isShopOpen = false;
        ShopMenuUI.SetActive(false);
        InputManager.playerInput.Enable();
        GUI_Input.playerInput.UI.OpenMap.Enable();
    }
    public void ShowBuyUI() => BuyUI.SetActive(true);
    public void HideBuyUI()
    {
        BuyUI.SetActive(false);
        SelectBtn(buybtn);
    }
    public void ShowSellUI() => SellUI.SetActive(true);
    public void HideSellUI()
    {
        SellUI.SetActive(false);
        SelectBtn(sellbtn);
    }
    public void SelectBtn(Button btn)
    {
        if (btn != null)
        {
            btn.Select();
            btn.OnSelect(null);
        }
    }
}
