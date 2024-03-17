using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Button Item_btn;
    public Image icon;
    public TextMeshProUGUI stackItem_text;
    public ItemSO item;
    public int GetItemValue;

    [SerializeField] public Image rarityFrame;
    [SerializeField] private Sprite rarityFrameCommon;
    [SerializeField] private Sprite rarityFrameRare;
    [SerializeField] private Sprite rarityFrameEpic;
    [SerializeField] private Sprite rarityFrameLegendary;

    private void Start()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    public void AddItem(ItemSO newItemSO, int _value)
    {
        item = newItemSO;
        icon.sprite = item.icon;
        icon.enabled = true;
        GetItemValue = _value;
        SetRarityFrameSlot(newItemSO.Rarity);
        if (item.currentAmt > 1) // stack
        {
            stackItem_text.text = "" + item.currentAmt;
            return;
        }
        if (item.currentAmt < 1)
        {
            stackItem_text.text = "";
            return;
        }
    }
    private void SetRarityFrameSlot(ItemRarity itemRarity)
    {
        rarityFrame.enabled = true;
        rarityFrame.sprite = itemRarity switch      //  same way with ( Switch - Case )
        {
            ItemRarity.Common => rarityFrameCommon,
            ItemRarity.Rare => rarityFrameRare,
            ItemRarity.Epic => rarityFrameEpic,
            ItemRarity.Legendary => rarityFrameLegendary,
            _ => rarityFrame.sprite
        };
    }
    public void ClearSlot()
    {
        item = null;
        rarityFrame.enabled = false;
        icon.sprite = null;
        icon.enabled = false;
        stackItem_text.text = "";
    }
    public void SetAmountText(string _value) => stackItem_text.text = _value.ToString();
    public void SelectItem()
    {
        GetComponentInParent<InventoryUI>().SelectItem(this);
    }
}
