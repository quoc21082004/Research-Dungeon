using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;
public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public TextMeshProUGUI gold_text;
    public Button back_btn;

    public ItemOptions itemOptionsWindow;
    public SelectedItemDisplay selectItemDisplay;
    public AmtConfirmWindow amtConfirmWindow;

    public static ItemSO selectedItem;
    //public static SpellBook
    protected Inventory inventory;
    protected List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] InventorySlot slotprefab;
    private readonly Dictionary<ItemSO, int> itemData = new Dictionary<ItemSO, int>();
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
        selectedItem = slot.item;
        selectItemDisplay.UpdateUI();
    }
    public void AddGoldFree()
    {
        int random = Random.Range(500, 800);
        PartyController.AddGold(random);
        gold_text.text = "" + inventory.Gold;
        AudioManager.instance.PlaySfx("Purchase");
    }
    public void OnSelectSortOption(int index)
    {
        switch (index)
        {
            case 1:
                SortByNumber(inventory.items);      // sort account item
                break;
            case 2:
                SortByRarity(inventory.items);      // sort rarity
                break;
            case 3:
                SortByType(inventory.items);        // sort type
                break;
            default:
                break;
        }
        itemData.Clear();
        /*foreach (var guiItem in guiSlot)
        {
            var itemSO = new ItemSO
            {
                nameItem = guiItem.item.nameItem,
                icon = guiItem.item.icon,
                itemNumber = guiItem.item.itemNumber,
                currentAmt = guiItem.item.currentAmt,
                isStackable = guiItem.item.isStackable,
                itemDescription = guiItem.item.itemDescription,
                flavor = guiItem.item.flavor,
                buyPrice = guiItem.item.buyPrice,
                sellPrice = guiItem.item.sellPrice,
                Rarity = guiItem.item.Rarity,
                Type = guiItem.item.Type,
            };
            itemData.Add(itemSO, guiItem.GetItemValue);
        }
        SortItem(itemData);*/
        LoadOldSlot();
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
    private void SortByRarity(List<ItemSO> itemSlots)
    {
        itemSlots.Sort((s1, s2) => s2.Rarity.CompareTo(s1.Rarity));
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].icon.sprite = itemSlots[i].icon;
                slots[i].stackItem_text.text = "" + itemSlots[i].currentAmt;
            }
        }
    }
    private void SortByType(List<ItemSO> itemSlots)
    {
        itemSlots.Sort((s1, s2) => s2.Type.CompareTo(s1.Type));
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].icon.sprite = itemSlots[i].icon;
                slots[i].stackItem_text.text = "" + itemSlots[i].currentAmt;
            }
        }
    }
    private void SortItem(Dictionary<ItemSO, int> dataItem)
    {
        foreach (var guiItem in slots.Where(item => item.gameObject.activeSelf))
        {
            if (!dataItem.Any())
                return;
            var keyValuepair = dataItem.First();        // convert dictionary into keyvaluePair
            guiItem.AddItem(keyValuepair.Key,keyValuepair.Value);
            dataItem.Remove(keyValuepair.Key);
        }
    }
    private void LoadOldSlot()
    {
        UpdateUI();
    }
}
