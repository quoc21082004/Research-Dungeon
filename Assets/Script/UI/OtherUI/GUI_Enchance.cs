using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class GUI_Enchance : MonoBehaviour , IGUI
{
    #region Variable
    const int defaultSlot = -1;
    int currentEquipIndex;
    private int equiptmentSlot;
    public EquipSlot equipmentprefab;
    public EquipSlot currentSelectEquipment;
    public Transform contentEquip;
    private List<EquipSlot> slotEquips;

    public Transform contentItemRequire;
    public InventorySlot slotprefab;
    private List<InventorySlot> slotItemRequire;

    public Slider expProgressSlider;
    public TextMeshProUGUI currency_txt;
    public TextMeshProUGUI equipLvl_txt;

    public Button accept_btn;
    public Button cancel_btn;

    private Coroutine updateCoroutine;
    private EquipmentUpgradeSO equipmentUpgradeSO;
    private int costUpgrade;
    private int equipmentLevel;
    private bool canUpgrade;
    private int increaseAtk => currentSelectEquipment.level + 5;
    private int increaseArmor => currentSelectEquipment.level + 3;
    #endregion

    #region Main Method
    private void Awake()
    {
        equipmentUpgradeSO = GameManager.instance.equipUpgradeSO;
        equiptmentSlot = Enum.GetNames(typeof(EquipmentSlot)).Length;
        for (int i = 0; i < equiptmentSlot; i++)
        {
            var slotprefab = PoolManager.instance.Release(equipmentprefab.gameObject);
            slotprefab.transform.SetParent(contentEquip);
            slotprefab.GetComponentInChildren<InventorySlotBtn>().enabled = false;
            slotprefab.GetComponentInChildren<ClickItemOption>().enabled = false;
        }
        slotEquips = contentEquip.GetComponentsInChildren<EquipSlot>().ToList();

        currentSelectEquipment.GetComponentInChildren<InventorySlotBtn>().enabled = false;
        currentSelectEquipment.GetComponentInChildren<ClickItemOption>().enabled = false;
        currentSelectEquipment.GetComponent<EquipSlot>().equipInfo.enabled = false;

        for (int i = 0; i < equipmentUpgradeSO.requireData[equipmentLevel].requireItems.Count; i++)
        {
            var slots = PoolManager.instance.Release(slotprefab.gameObject);
            slots.transform.SetParent(contentItemRequire);
            slots.GetComponentInChildren<InventorySlotBtn>().enabled = false;
            slots.GetComponentInChildren<ClickItemOption>().enabled = false;
        }
        slotItemRequire = contentItemRequire.GetComponentsInChildren<InventorySlot>().ToList();
        expProgressSlider.minValue = 1;
        expProgressSlider.maxValue = equipmentUpgradeSO.MAX_LEVEL;
        expProgressSlider.value = equipmentLevel;
    }
    private void OnEnable() => RegisterEvent();
    private void OnDisable() => UnRegisterEvent();
    #endregion

    #region Resurb Method
    private void RegisterEvent()
    {
        GUI_Manager.AddGUI(this);
        slotEquips = contentEquip.GetComponentsInChildren<EquipSlot>().ToList();
        slotEquips.ForEach(x => x.gameObject.SetActive(true));
        slotItemRequire.ForEach(x => x.gameObject.SetActive(false));

        accept_btn.onClick.AddListener(OnClickUpgradeButton);
        cancel_btn.onClick.AddListener(OnClickCancelButton);

        var _currentEquipment = EquipmentManager.instance;
        for (int i = 0; i < slotEquips.Count; i++)
        {
            if (_currentEquipment.currentEquipment[i] != null)
            {
                slotEquips[i].AddItem(_currentEquipment.currentEquipment[i], 0);
                slotEquips[i].GetItemValue = i;
                var copy = i;
                slotEquips[i].GetComponent<EquipSlot>().equipInfo.text
                    = $"{_currentEquipment.currentEquipment[i].nameItem}   + {_currentEquipment.currentEquipment[i].level}";
                slotEquips[i].GetComponentInChildren<Button>().onClick.AddListener(() =>
                {
                    OnSelectItem(copy);
                });
            }
            else if (_currentEquipment.currentEquipment[i] == null)
            {
                slotEquips[i].ClearSlot();
                slotEquips[i].GetItemValue = defaultSlot;
                var copy = i;
                slotEquips[i].GetComponent<EquipSlot>().equipInfo.text = "";
                slotEquips[copy].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            }
        }
        UpdateDataEC();
    }
    private void UnRegisterEvent()
    {
        GUI_Manager.RemoveGUI(this);
        slotEquips = contentEquip.GetComponentsInChildren<EquipSlot>().ToList();
        var _currentEquipment = EquipmentManager.instance;
        for (int i = 0; i < slotEquips.Count; i++)
        {
            if (_currentEquipment.currentEquipment[i] != null)
            {
                var copy = i;
                slotEquips[copy].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            }
        }
        slotItemRequire.ForEach(x => x.gameObject.SetActive(false));
        accept_btn.onClick.RemoveListener(OnClickUpgradeButton);
        cancel_btn.onClick.RemoveListener(OnClickCancelButton);
        currentSelectEquipment.ClearSlot();
        costUpgrade = 0;
    }
    private void UpdateDataEC()
    {
        SetCoinText();
        SetUpgradeStateButton();
        SetBarProgressLevel();
        SetEquipLevelText();
    }
    private void OnClickUpgradeButton()
    {
        if (updateCoroutine != null)
            StopCoroutine(updateCoroutine);
        updateCoroutine = StartCoroutine(UpdateCoroutine());
    }
    private void OnClickCancelButton()
    {

    }
    private void OnSelectItem(int _index)
    {
        if (EquipmentManager.instance.currentEquipment[_index] == null)
        {
            currentSelectEquipment.ClearSlot();
            return;
        }
        currentEquipIndex = _index;
        currentSelectEquipment.GetItemValue = _index;
        currentSelectEquipment.AddItem(EquipmentManager.instance.currentEquipment[_index], 0);
        currentSelectEquipment.level = EquipmentManager.instance.currentEquipment[_index].level;
        equipmentLevel = currentSelectEquipment.level;
        slotEquips[_index].equipInfo.text = $"{EquipmentManager.instance.currentEquipment[_index].nameItem}   + {EquipmentManager.instance.currentEquipment[_index].level}";
        UpdateDataEC();
        ShowItemRequire();
    }
    private void ShowItemRequire()
    {
        canUpgrade = true;
        if (equipmentLevel >= equipmentUpgradeSO.MAX_LEVEL)
        {
            canUpgrade = false;
            return;
        }
        slotItemRequire.ForEach(x => x.gameObject.SetActive(true));
        costUpgrade = equipmentUpgradeSO.requireData[equipmentLevel].cost;
        var upgradeInfo = equipmentUpgradeSO.requireData[equipmentLevel].requireItems;
        int count = 0;
        foreach (var spawnItem in slotItemRequire)
        {
            spawnItem.AddItem(upgradeInfo[count].requireItem, 0);
            int hasItem = PartyController.inventoryG.GetItemAmt(upgradeInfo[count].requireItem);
            int needItem = upgradeInfo[count].value;
            spawnItem.stackItem_text.color = hasItem >= needItem ? Color.white : hasItem < needItem ? Color.red : Color.white;
            spawnItem.SetAmountText($"{hasItem}/{needItem}");
            if (hasItem < needItem)
                canUpgrade = false;
            count++;
        }
        UpdateDataEC();
    }
    private IEnumerator UpdateCoroutine()
    {
        var currentLvl = Mathf.Clamp(equipmentLevel + 1, 0, equipmentUpgradeSO.MAX_LEVEL);
        var currentAtk = EquipmentManager.instance.currentEquipment[currentEquipIndex].atkModifier + increaseAtk;
        var currentArmor = EquipmentManager.instance.currentEquipment[currentEquipIndex].armorModifier + increaseArmor;

        #region Upgrade Notice

        UpgradeNoticeManager.instance.MAX_ATTRIBUTE = 2;
        UpgradeNoticeManager.instance.SpawnNoticeUpgrade();
        UpgradeNoticeManager.instance.SetLevelText(currentLvl.ToString());
        UpgradeNoticeManager.instance.CreateNoticeBar(0, "Atk", (currentAtk - increaseAtk).ToString(), currentAtk.ToString());
        UpgradeNoticeManager.instance.CreateNoticeBar(1, "Defense", (currentArmor - increaseArmor).ToString(), currentArmor.ToString());
        PartyController.IncreaseCoin(-costUpgrade);
        #endregion

        foreach (var _lfItem in equipmentUpgradeSO.requireData[equipmentLevel - 1].requireItems)    
        {
            var _countIndex = _lfItem.value;
            PartyController.inventoryG.Remove(_lfItem.requireItem, _countIndex);
        }
        EquipmentManager.instance.currentEquipment[currentEquipIndex].level = currentLvl;
        EquipmentManager.instance.currentEquipment[currentEquipIndex].atkModifier = currentAtk;
        EquipmentManager.instance.currentEquipment[currentEquipIndex].armorModifier = currentArmor;

        OnSelectItem(currentEquipIndex);
        UpdateDataEC();
        yield return null;
    }
    #endregion

    #region Set
    private void SetCoinText()
    {
        int currentCoin = PartyController.inventoryG.Gold;
        currency_txt.color = currentCoin >= costUpgrade ? Color.white : Color.red;
        currency_txt.text = $"{currentCoin}/{costUpgrade}";
    }
    private void SetEquipLevelText()
    {
        var equipAfterUpgrade = "";
        equipAfterUpgrade = currentSelectEquipment.level < equipmentUpgradeSO.MAX_LEVEL && canUpgrade ? "+ 1"
            : currentSelectEquipment.level >= equipmentUpgradeSO.MAX_LEVEL ? "+ MAX" : "";
        equipLvl_txt.text = $"Lv.{currentSelectEquipment.level}   {equipAfterUpgrade}";
    }
    private void SetUpgradeStateButton()    
    {
        accept_btn.interactable = true ?
             (canUpgrade && PartyController.inventoryG.Gold >= costUpgrade && currentSelectEquipment.level < equipmentUpgradeSO.MAX_LEVEL)
            : (!canUpgrade && PartyController.inventoryG.Gold < costUpgrade && currentSelectEquipment.level >= equipmentUpgradeSO.MAX_LEVEL);
    }
    private void SetBarProgressLevel() => expProgressSlider.value = currentSelectEquipment.level <= equipmentUpgradeSO.MAX_LEVEL ? currentSelectEquipment.level : expProgressSlider.maxValue;
    #endregion

    #region Interface Method
    public void GetReference(GameManager _gameManager) { }
    public void UpdateDataGUI()
    {

    }
    #endregion
}
