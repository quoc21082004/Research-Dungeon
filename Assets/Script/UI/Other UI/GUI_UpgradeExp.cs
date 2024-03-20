using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Net;
using System.Linq;

public class GUI_UpgradeExp : MonoBehaviour
{
    [Header(("Stats Character"))]
    [SerializeField] TextMeshProUGUI charLevel_txt;
    [SerializeField] TextMeshProUGUI charExp_txt;
    [SerializeField] TextMeshProUGUI currency_txt;
    [SerializeField] TextMeshProUGUI itemQuantity_txt;
    [SerializeField] Slider mainExpSliderBar;
    [SerializeField] Slider backExpSliderBar;

    [Header("Item Buff")]
    [SerializeField] ExperienceSO smallExpBuff;
    [SerializeField] ExperienceSO mediumExpBuff;
    [SerializeField] ExperienceSO bigExpBuff;

    [Header("Item View")]
    [SerializeField] TextMeshProUGUI smallExpBuff_txt;
    [SerializeField] TextMeshProUGUI mediumExpBuff_txt;
    [SerializeField] TextMeshProUGUI bigExpBuff_txt;

    [SerializeField] InventorySlot itemprefab;

    public List<ExperienceSO> upgrade;
    private List<InventorySlot> itemUpgrade;
    [SerializeField] Transform itemParents;
    [SerializeField] Image gradientItem;

    [SerializeField] Button increaseAmountUse_btn;
    [SerializeField] Button decreaseAmountUse_btn;
    [SerializeField] Button upgrade_btn;
    [SerializeField] Button cancel_btn;
    public CharacterUpgradeSO upgradeDataSO;

    private int increaseLevel;
    private int increaseExp;
    private int increaseHp => 50 * increaseLevel;
    private int increaseMp => 15 * increaseLevel;
    private int increaseDef => 5 * increaseLevel;
    private int increaseAtk => 5 * increaseLevel;

    private int currentCoin,totalCost, totalExp, amountUse, selectItem = 0;
    private int smallExpAmt, mediumExpAmt, bigExpAmt, maxLvl,currentLvl, currentExp, maxExp = 0;
    private bool canUpgrade => currentLvl < maxLvl;
    private void OnEnable()
    {
        InitValue();
        upgrade_btn.onClick.AddListener(OnClickUpgradeButton);
        cancel_btn.onClick.AddListener(OnClickCancelButton);

        for (int i = 0; i < upgrade.Count; i++)
        {
            var spawnItemUpgrade = PoolManager.instance.Release(itemprefab.gameObject);
            spawnItemUpgrade.transform.SetParent(itemParents);
            spawnItemUpgrade.GetComponentInChildren<InventorySlotBtn>().enabled = false;
            spawnItemUpgrade.GetComponentInChildren<ClickItemOption>().enabled = false;
        }
        itemUpgrade = itemParents.GetComponentsInChildren<InventorySlot>().ToList();
        itemUpgrade.ForEach(s1 => s1.GetComponentInChildren<Button>().onClick.RemoveAllListeners());
        int count = 0;
        foreach (var _itemUpgrade in itemUpgrade)
        {
            var itemSO = upgrade[count];
            var itemValue = PartyController.inventoryG.GetItemAmt(itemSO);
            _itemUpgrade.AddItem(itemSO, itemValue);
            _itemUpgrade.SetAmountText("");
            count++;
        }
        itemUpgrade[0].GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            OnSelectItemButton(1);
        });
        itemUpgrade[1].GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            OnSelectItemButton(2);
        });
        itemUpgrade[2].GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            OnSelectItemButton(3);
        });
    }
    private void OnDisable()
    {
        upgrade_btn.onClick.RemoveListener(OnClickUpgradeButton);
        cancel_btn.onClick.RemoveListener(OnClickCancelButton);
        itemUpgrade.ForEach(s1 => s1.GetComponentInChildren<Button>().onClick.RemoveAllListeners());          
        itemUpgrade.ForEach(_itemUpgrade => _itemUpgrade.gameObject.SetActive(false));
    }
    private void InitValue()
    {
        var playerdata = PartyController.player.playerdata;
        increaseExp = 0;
        increaseLevel = 0;
        totalCost = 0;
        amountUse = 0;
        selectItem = 0;
        increaseAmountUse_btn.interactable = false;
        decreaseAmountUse_btn.interactable = false;

        maxLvl = upgradeDataSO.Data.Count;
        currentCoin = playerdata.otherStats.gold;
        UpdateData();
    }
    private void UpdateData()
    {
        GetStats();
        SetItemQuantity();
        SetCoinText();
        SetAmountUseText();
        SetLevelText();
        SetUpgradeStateButton();
        SetExpText();
        CheckLevelMax();
    }
    private void OnClickUpgradeButton()
    {
        SetStats();
        InitValue();
        SetCoinText();
        UpdateData();
    }
    public void OnClickCancelButton()
    {
        if (amountUse > 0)
        {
            InitValue();
            UpdateData();
            return;
        }
    }
    public void OnIncreaseAmountButton(int _value)
    {
        amountUse += _value;
        decreaseAmountUse_btn.interactable = amountUse > 0;
        switch (selectItem)
        {
            case 1:
                increaseAmountUse_btn.interactable = smallExpAmt > amountUse && canUpgrade;
                totalCost = smallExpBuff.costUpgrade * amountUse;
                increaseExp = smallExpBuff.value;
                break;
            case 2:
                increaseAmountUse_btn.interactable = mediumExpAmt > amountUse && canUpgrade;
                totalCost = mediumExpBuff.costUpgrade * amountUse;
                increaseExp = mediumExpBuff.value;
                break;
            case 3:
                increaseAmountUse_btn.interactable = bigExpAmt > amountUse && canUpgrade;
                totalCost = bigExpBuff.costUpgrade * amountUse;
                increaseExp = bigExpBuff.value;
                break;
            default:
                break;
        }
        totalExp = amountUse * increaseExp;
        ProgressBarBeforeUpdate();
        SetAmountUseText();
        SetItemQuantity();
        SetLevelText();
        SetCoinText();
        SetExpText();
        SetUpgradeStateButton();
    }
    public void OnDecreaseAmountButton(int _value)
    {
        amountUse -= _value;
        switch (selectItem)
        {
            case 1:
                decreaseAmountUse_btn.interactable = amountUse > 0 && canUpgrade; 
                totalCost = smallExpBuff.costUpgrade * amountUse;
                increaseExp = smallExpBuff.value;
                break;
            case 2:
                decreaseAmountUse_btn.interactable = amountUse > 0 && canUpgrade;
                totalCost = mediumExpBuff.costUpgrade * amountUse;
                increaseExp = mediumExpBuff.value;
                break;
            case 3:
                decreaseAmountUse_btn.interactable = amountUse > 0 && canUpgrade;
                totalCost = bigExpBuff.costUpgrade * amountUse;
                increaseExp = bigExpBuff.value;
                break;
            default:
                break;
        }
        totalExp = increaseExp * amountUse;

        ProgressBarBeforeUpdate();
        SetAmountUseText();
        SetItemQuantity();
        SetLevelText();
        SetCoinText();
        SetExpText();
        SetUpgradeStateButton();
    }
    public void OnSelectItemButton(int _value)
    {
        InitValue();
        selectItem = _value;
        OnIncreaseAmountButton(0);
        if (!canUpgrade)
            return;
        Debug.Log("select item :" + selectItem);
    }
    private void SetStats()
    {
        var playerSO = PartyController.player.playerdata;
        var curLvl = playerSO.upgradeLevel.level + increaseLevel;
        var curHp = playerSO.basicStats.health + increaseHp;
        var curMp = playerSO.basicStats.mana + increaseMp;
        var curDef = playerSO.basicStats.defense + increaseDef;

        // notice when upgrade

        PartyController.IncreaseCoin(-totalCost);
        playerSO.upgradeLevel.exp = backExpSliderBar.value;
        playerSO.upgradeLevel.level = curLvl;
        playerSO.basicStats.health = curHp;
        playerSO.basicStats.mana = curMp;
        playerSO.basicStats.defense = curDef;
        GameManager.instance.LevelUp();
        switch (selectItem)
        {
            case 1: // small exp
                PartyController.inventoryG.Remove(smallExpBuff, amountUse);
                break;
            case 2: // medium exp
                PartyController.inventoryG.Remove(mediumExpBuff, amountUse);
                break;
            case 3: // big exp
                PartyController.inventoryG.Remove(bigExpBuff, amountUse);
                break;
            default:
                break;
        }
    }
    private void GetStats()
    {
        var playerdata = PartyController.player.playerdata;
        currentLvl = playerdata.upgradeLevel.level;
        currentExp = (int)playerdata.upgradeLevel.exp;
        maxExp = (int)upgradeDataSO.Data[currentLvl - 1].expToLvl;

        mainExpSliderBar.maxValue = maxExp;
        mainExpSliderBar.minValue = 0;
        mainExpSliderBar.value = currentExp;

        backExpSliderBar.maxValue = maxExp;
        backExpSliderBar.minValue = 0;
        backExpSliderBar.value = currentExp;
    }
    private void ProgressBarBeforeUpdate()
    {
        var playerSO = PartyController.player.playerdata;
        increaseLevel = 0;
        if (amountUse == 0)
        {
            backExpSliderBar.maxValue = mainExpSliderBar.maxValue;
            backExpSliderBar.value = mainExpSliderBar.value;
            return;
        }
        var hasCharacterExp = playerSO.upgradeLevel.exp;
        var totalIncreaseExp = hasCharacterExp + currentExp + totalExp;
        if (totalIncreaseExp >= upgradeDataSO.Data[currentLvl].expToLvl) 
        {
            increaseLevel++;
            backExpSliderBar.maxValue = upgradeDataSO.Data[currentLvl + 1].expToLvl;
            backExpSliderBar.value = (currentExp + totalExp) - upgradeDataSO.Data[currentLvl].expToLvl;
        }
        var remainingExp = totalIncreaseExp - upgradeDataSO.Data[currentLvl].expToLvl;
        backExpSliderBar.value = currentExp + remainingExp;
    }
    private void SetItemQuantity()
    {
        smallExpAmt = PartyController.inventoryG.GetItemAmt(smallExpBuff);
        mediumExpAmt = PartyController.inventoryG.GetItemAmt(mediumExpBuff);
        bigExpAmt = PartyController.inventoryG.GetItemAmt(bigExpBuff);

        smallExpBuff_txt.text = smallExpAmt > 0 ? $"<color=white>{smallExpAmt.ToString()}" : $"<color=red>{smallExpAmt.ToString()}";
        mediumExpBuff_txt.text = mediumExpAmt > 0 ? "<color=white>" + mediumExpAmt.ToString() : "<color=red>" + mediumExpAmt.ToString();
        bigExpBuff_txt.text = bigExpAmt > 0 ? "<color=white>" + bigExpAmt.ToString() : "<color=red>" + bigExpAmt.ToString();

    }
    private void SetCoinText()
    {
        currency_txt.color = currentCoin >= totalCost ? Color.white : Color.red;
        currency_txt.text = $"{currentCoin}/{totalCost}";
    }
    private void SetAmountUseText() => itemQuantity_txt.text = $"{amountUse}";
    private void SetLevelText()
    {
        var levelAfterUpgrade = "";
        if (canUpgrade)
            levelAfterUpgrade = increaseLevel == 0 ? "" : "+" + increaseLevel;
        else
            levelAfterUpgrade = "MAX";
        charLevel_txt.text = $"Lv.{currentLvl}   {levelAfterUpgrade}";
    }
    private void SetExpText()
    {
        var totalExpAfterUpgrade = "";
        if (canUpgrade)
            totalExpAfterUpgrade = totalExp == 0 ? "" : $"+ {totalExp}";
        else
            totalExpAfterUpgrade = "";
        charExp_txt.text = $" {totalExpAfterUpgrade}    {currentExp}/{maxExp}";
    }
    private void CheckLevelMax()
    {
        if (currentLvl < maxLvl)
            return;
        currentExp = 0;
        mainExpSliderBar.value = mainExpSliderBar.maxValue;
        charExp_txt.text = "????";
    }
    private void SetUpgradeStateButton() => upgrade_btn.interactable = (amountUse > 0 && currentCoin >= totalCost && canUpgrade);

}
