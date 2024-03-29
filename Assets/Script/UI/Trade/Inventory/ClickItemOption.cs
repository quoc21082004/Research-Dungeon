using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickItemOption : MonoBehaviour, IPointerClickHandler
{
    public ItemOptions itemOptionWindow;
    private InventorySlot slotPos;
    private void Start()
    {
        slotPos = GetComponentInParent<InventorySlot>();
        if (itemOptionWindow != null)
            itemOptionWindow = GetComponentInParent<InventoryUI>().itemOptionsWindow;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable = true && (eventData.button == PointerEventData.InputButton.Right
            || eventData.button == PointerEventData.InputButton.Left)) 
        {
            GetComponentInParent<InventorySlot>().SelectItem();

            if (InventoryUI.selectedItem != null)
            {
                itemOptionWindow.gameObject.SetActive(true);
                itemOptionWindow.transform.position = slotPos.transform.position + new Vector3(30f, 0f, 0f);
                itemOptionWindow.selectSlotbtn = GetComponentInChildren<Button>();
            }
            else
                itemOptionWindow.gameObject.SetActive(false);
                  // if item not equip
        }
    }
}
