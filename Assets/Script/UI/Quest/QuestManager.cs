using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestManager : Singleton<QuestManager>
{
    public int currentQuest;    
    public int maxQuest => 3;

    public List<QuestSetUp> questList = new List<QuestSetUp>();

    private void Start()
    {
        var quests = Resources.LoadAll<QuestSetUp>("Quest");
        questList.Clear();
        foreach (var _questList in quests)
        {
            questList.Add(Instantiate(_questList));
        }
        currentQuest = 0;
        var taskQuest = questList.Select(x => x.taskQuest);
        foreach (var questSortItem in questList)
        {
            SortItemReward(questSortItem.rewardQuest);
        }
    }
    public void OnStartQuest(QuestSetUp questSetUp)
    {
        currentQuest = Mathf.Clamp(currentQuest + 1, 0, maxQuest);
        var taskQuest = questSetUp.taskQuest;
        taskQuest.isCompleted = false;
        taskQuest.isLocked = false;
        taskQuest.isReceived = true;
    }
    public void OnCompleteQuest(QuestSetUp questSetUp)
    {
        currentQuest = Mathf.Clamp(currentQuest - 1, 0, maxQuest);
        foreach (var questReward in questSetUp.rewardQuest)
        {
            RewardManager.instance.GetRewardFromQuest(questReward);
        }
        PartyController.IncreaseCoin(questSetUp.coinReward);
        var taskQuest = questSetUp.taskQuest;
        taskQuest.isCompleted = true;    //true;
        taskQuest.isLocked = false;         // true;
        taskQuest.isReceived = true;         //false;
    }
    public void OnCancelQuest(QuestSetUp questSetUp)
    {
        currentQuest = Mathf.Clamp(currentQuest - 1, 0, maxQuest);
        var taskQuest = questSetUp.taskQuest;
        taskQuest.isCompleted = false;
        taskQuest.isLocked = false;     //false;
        taskQuest.isReceived = false;
    }
    private void SortItemReward(List<ItemReward> itemRewards) => itemRewards.Sort((r1, r2) => r1.item.Rarity.CompareTo(r2.item.Rarity));
}
