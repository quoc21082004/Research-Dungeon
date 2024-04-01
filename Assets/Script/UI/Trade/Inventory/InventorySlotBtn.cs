using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotBtn :MonoBehaviour ,ISelectHandler
{
    public ItemOptions itemOptionWindow;
    private void Start()
    {
        itemOptionWindow = GetComponentInParent<InventoryUI>().itemOptionsWindow;
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (InventoryUI.selectedItem != null)
            {
                itemOptionWindow.gameObject.SetActive(true);
                itemOptionWindow.selectSlotbtn = GetComponent<Button>();
            }
            else
                itemOptionWindow.gameObject.SetActive(false);
        });
    }
    public void OnSelect(BaseEventData eventData)
    {
        //GetComponentInParent<InventorySlot>().SelectItem();   and plus button onclick at item button
        GetComponentInParent<Button>().onClick.AddListener(() =>
        {
            GetComponentInParent<InventorySlot>().SelectItem();
        });
    }
}
