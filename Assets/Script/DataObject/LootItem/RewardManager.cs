﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : Singleton<RewardManager>
{
    [Header("                       Exp Loots")]
    [SerializeField] LootOnWorld[] lootOnWorld;
    [Header("                       Item Loots")]
    [SerializeField] LootOnWorld[] itemLoots;
    [Header("                       Mob Loots")]
    [SerializeField] LootOnWorld[] mobLoots;
    [Header("                       Coin Loots")]
    [SerializeField] LootOnWorld[] coinLoots;
    [SerializeField] float genrange;

    private Coroutine waitCoroutine;
    public void GetRewardFromQuest(ItemReward _reward)
    {
        var rewardItem = _reward.item;
        var itemSO = Instantiate(rewardItem);
        itemSO.currentAmt = _reward.value;
        PartyController.inventoryG.AddItem(itemSO, itemSO.currentAmt);

        NoticeManager.instance.CreateNoticeLeftItem(rewardItem.icon, $"{rewardItem.nameItem} x{_reward.value}");
        NoticeManager.instance.EnableNoticeLeftItem();
    }
    public void SpawnLoot(TypeEnemy type ,Vector2 position)
    {
        foreach (var loot in lootOnWorld)
            loot.LootSpawn(position + Random.insideUnitCircle * genrange);
        foreach (var coinsloots in coinLoots)
            coinsloots.LootSpawn(position + Random.insideUnitCircle * genrange);
        for (int i = 0; i < itemLoots.Length; i++)
            itemLoots[i].LootSpawn(position + Random.insideUnitCircle * genrange);
        switch (type)                   // or use script in Drop Manager direct object
        {
            case TypeEnemy.LittleRange:
                mobLoots[0].LootSpawn(position + Random.insideUnitCircle * genrange);
                break;
            case TypeEnemy.Boar:
                mobLoots[1].LootSpawn(position + Random.insideUnitCircle * genrange);
                break;
            case TypeEnemy.Bat:
                mobLoots[2].LootSpawn(position + Random.insideUnitCircle * genrange);
                break;
            case TypeEnemy.FlyingMelee:
                mobLoots[4].LootSpawn(position + Random.insideUnitCircle * genrange);
                break;
            case TypeEnemy.Skeleton:
                mobLoots[5].LootSpawn(position + Random.insideUnitCircle * genrange);
                break;
            case TypeEnemy.Scopoion:
                mobLoots[6].LootSpawn(position + Random.insideUnitCircle * genrange);
                break;
            default:
                break;
        }

        if (waitCoroutine != null)
            StopCoroutine(WaitCoroutine());
        waitCoroutine = StartCoroutine(WaitCoroutine());
    }
    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(2f);
    }
}
