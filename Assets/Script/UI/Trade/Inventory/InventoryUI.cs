using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public TextMeshProUGUI gold_text;
    public Button back_btn;

    public ItemOptions itemOptionsWindow;
    public SelectedItemDisplay selectItemDisplay;
    public AmtConfirmWindow amtConfirmWindow;

    public static ItemSO selectedItem;
    protected Inventory inventory;
    protected List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] InventorySlot slotprefab;
    private void Awake()
    {
        inventory = PartyController.inventoryG;
        //slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>().ToList();
        for (int i = 0; i < inventory.space; i++)
        {
            Instantiate(slotprefab, itemsParent);
        }
        inventory.onItemChangedCallBack += UpdateUI;
    }
    void OnEnable()
    {
        back_btn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        if (inventory == null || inventory != PartyController.inventoryG)
        {
            inventory = PartyController.inventoryG;
            inventory.onItemChangedCallBack += UpdateUI;
        }
        slots = itemsParent.GetComponentsInChildren<InventorySlot>().ToList();
        selectItemDisplay.gameObject.SetActive(false);
        if (itemOptionsWindow != null)
            itemOptionsWindow.gameObject.SetActive(false);
        if (amtConfirmWindow != null)
            amtConfirmWindow.gameObject.SetActive(false);
        UpdateUI();
    }
    protected virtual void UpdateUI()
    {
        for (int i = 0; i < slots.Count; i++) 
        {
            if (i < inventory.items.Count)
                slots[i].AddItem(inventory.items[i], i);
            else
                slots[i].ClearSlot();
        }
        if (selectedItem != null && selectedItem.currentAmt <= 0)
            selectedItem = null;
        selectItemDisplay.UpdateUI();
        gold_text.text = "" + inventory.Gold;
    }
    public void SelectItem(InventorySlot slot) // make option show
    {
        if (itemOptionsWindow != null)
            itemOptionsWindow.gameObject.SetActive(false);
        selectedItem = slot.item;       // 415
        selectItemDisplay.transform.position = new Vector3(slot.transform.position.x - 172, slot.transform.position.y, slot.transform.position.z);
        selectItemDisplay.UpdateUI();

    }
    public void AddGoldFree()
    {
        int random = Random.Range(500, 800);
        PartyController.IncreaseCoin(random);
        gold_text.text = "" + inventory.Gold;
        AudioManager.instance.PlaySfx("Purchase");
    }
    private void SortByNumber(List<ItemSO> itemSlots)
    {
        itemSlots.Sort((s1, s2) => s2.currentAmt.CompareTo(s1.currentAmt));
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].icon.sprite = itemSlots[i].icon;
                slots[i].stackItem_text.text = "" + itemSlots[i].currentAmt;
            }
        }
    }
}
