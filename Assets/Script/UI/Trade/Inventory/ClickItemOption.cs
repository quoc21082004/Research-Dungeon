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
        itemOptionWindow = GetComponentInParent<InventoryUI>().itemOptionsWindow;
        slotPos = GetComponentInParent<InventorySlot>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable = true && (eventData.button == PointerEventData.InputButton.Right
            || eventData.button == PointerEventData.InputButton.Left)) 
        {
            GetComponentInParent<InventorySlot>().SelectItem();
            itemOptionWindow.gameObject.SetActive(true);
            itemOptionWindow.transform.position = slotPos.transform.position + new Vector3(30f,0f,0f);
            itemOptionWindow.selectSlotbtn = GetComponentInChildren<Button>();
        }
    }
}
