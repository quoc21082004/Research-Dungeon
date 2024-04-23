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
    [HideInInspector] public static Inventory inventory;
    protected List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] public InventorySlot slotprefab;

    public TMP_Dropdown sortDropDown;
    List<string> sort_txt_index = new List<string>() { "All", "Amount", "Rarity", "Type" };
    private string Key_CurrentSortIndex = "SortIndex";

    protected virtual void Awake()
    {
        inventory = PartyController.inventoryG;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>().ToList();
        for (int i = 0; i < inventory.space; i++)
        {
            var spawnSlot = PoolManager.instance.Release(slotprefab.gameObject);
            spawnSlot.transform.SetParent(itemsParent);
            spawnSlot.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        inventory.onItemChangedCallBack += UpdateUI;
    }
    protected virtual void OnEnable()
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
        slots.ForEach(x1 => x1.gameObject.SetActive(true));
        selectItemDisplay.gameObject.SetActive(false);
        if (itemOptionsWindow != null)
            itemOptionsWindow.gameObject.SetActive(false);
        if (amtConfirmWindow != null)
            amtConfirmWindow.gameObject.SetActive(false);

        PlayerPrefs.SetInt(Key_CurrentSortIndex, 0);
        SortByAll(inventory.items);
        InitSortDropDown();
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
        {
            itemOptionsWindow.gameObject.SetActive(false);
            selectedItem = slot.item;       // 415
            selectItemDisplay.transform.position = new Vector3(slot.transform.position.x - 172, slot.transform.position.y, slot.transform.position.z);
            selectItemDisplay.UpdateUI();
        }
    }

    #region Sort Item ( acorrding amonut - rarity - type
    private void InitSortDropDown()
    {
        if (sortDropDown)
        {
            sortDropDown.ClearOptions();
            sortDropDown.AddOptions(sort_txt_index);
            sortDropDown.value = PlayerPrefs.GetInt(Key_CurrentSortIndex, 0);
            sortDropDown.RefreshShownValue();
            SortItem(sortDropDown.value);
        }
    }
    public void SortItem(int _selectOption) // item Sort Inventory
    {
        PlayerPrefs.SetInt(Key_CurrentSortIndex, _selectOption);
        switch(_selectOption)
        {
            case 0:
                SortByAll(inventory.items);
                break;
            case 1:
                SortByNumber(inventory.items);
                break;
            case 2:
                SortByRarity(inventory.items);
                break;
            case 3:
                SortByType(inventory.items);
                break;
            default:
                break;
        }
    }
    private void SortByAll(List<ItemSO> itemSlots)
    {
        AudioManager.instance.PlaySfx("Click");
        itemSlots.Sort((s1, s2) => s1.itemNumber.CompareTo(s2.itemNumber));
        UpdateUI();
    }
    private void SortByNumber(List<ItemSO> itemSlots)
    {
        AudioManager.instance.PlaySfx("Click");
        itemSlots.Sort((s1, s2) => s2.currentAmt.CompareTo(s1.currentAmt));
        UpdateUI();
    }
    private void SortByRarity(List<ItemSO> itemSlots)
    {
        AudioManager.instance.PlaySfx("Click");
        itemSlots.Sort((s1, s2) => s2.Rarity.CompareTo(s1.Rarity));
        UpdateUI();
    }
    private void SortByType(List<ItemSO> itemSlots)
    {
        AudioManager.instance.PlaySfx("Click");
        itemSlots.Sort((s1, s2) => s1.Type.CompareTo(s2.Type));
        UpdateUI();
    }

    #endregion

    public void AddGoldFree()
    {
        int random = Random.Range(500, 800);
        PartyController.IncreaseCoin(random);
        gold_text.text = "" + inventory.Gold;
        AudioManager.instance.PlaySfx("Purchase");
    }
}