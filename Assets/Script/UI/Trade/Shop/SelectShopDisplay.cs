using UnityEngine;
using TMPro;
public class SelectShopDisplay : SelectedItemDisplay
{ 
    public TextMeshProUGUI itemPriceGold_txt;
    private void OnEnable() => UpdateUI();
    public override void UpdateUI()
    {
        base.UpdateUI();
        if (InventoryUI.selectedItem != null)
        {
            if (PartyController.inventoryG.Gold >= InventoryUI.selectedItem.buyPrice)
                itemPriceGold_txt.color = Color.white;
            else if (PartyController.inventoryG.Gold < InventoryUI.selectedItem.buyPrice)
                itemPriceGold_txt.color = Color.red;
            itemPriceGold_txt.text = "" + InventoryUI.selectedItem.buyPrice + "  <sprite=3>";
        }
        else if (InventoryUI.selectedItem == null)
            itemPriceGold_txt.text = "";
    }
}
