using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public int currentQuest;    
    public int maxQuest => 3;
    private List<QuestSetUp> questList = new();
    private void Start()
    {

    }
    public void OnStartQuest(QuestSetUp questSetUp)
    {
        currentQuest = Mathf.Clamp(currentQuest + 1, 0, maxQuest);

        questSetUp.taskQuest.isCompleted = false;
        questSetUp.taskQuest.isLocked = false;
        questSetUp.taskQuest.isReceived = false;
    }
    public void OnCompleteQuest(QuestSetUp questSetUp)
    {

    }
    public void OnCancelQuest(QuestSetUp questSetUp)
    {
        throw new NotImplementedException();
    }
    private void SortItemReward(List<ItemReward> itemRewards)
    {
        itemRewards.Sort((r1, r2) => r1.item.Rarity.CompareTo(r1.item.Rarity));
    }
}
