using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SelectedItemDisplay : MonoBehaviour
{
    [SerializeField] public Image item_img;
    [SerializeField] public TextMeshProUGUI itemTitle_txt;
    [SerializeField] public TextMeshProUGUI itemDescription_txt;
    [SerializeField] public TextMeshProUGUI itemFlavor_txt;
    private void OnEnable()
    {
        UpdateUI();
    }
    public virtual void UpdateUI()
    {
        if (InventoryUI.selectedItem == null)
        {
            gameObject.SetActive(false);
            item_img.sprite = null;
            itemTitle_txt.text = "";
            itemDescription_txt.text = "";
            itemFlavor_txt.text = "";
        }
        else if (InventoryUI.selectedItem != null)
        {
            gameObject.SetActive(true);
            item_img.sprite = InventoryUI.selectedItem.icon;
            itemTitle_txt.text = InventoryUI.selectedItem.nameItem;
            itemDescription_txt.text = InventoryUI.selectedItem.itemDescription;
            itemFlavor_txt.text = InventoryUI.selectedItem.flavor;
        }
    }
}
