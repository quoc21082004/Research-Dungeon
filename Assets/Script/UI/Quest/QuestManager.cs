using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        var taskQuest = questList.Select(x => x.GetTask());
        foreach (var questSortItem in questList)
        {
            SortItemReward(questSortItem.GetRewardQuest());
        }
    }
    public void OnStartQuest(QuestSetUp questSetUp)
    {
        currentQuest = Mathf.Clamp(currentQuest + 1, 0, maxQuest);
        var taskQuest = questSetUp.GetTask();
        taskQuest.SetComplete(false);
        taskQuest.SetLocked(false);
        taskQuest.SetReceived(true);
    }
    public void OnCompleteQuest(QuestSetUp questSetUp)
    {
        currentQuest = Mathf.Clamp(currentQuest - 1, 0, maxQuest);
        foreach (var questReward in questSetUp.GetRewardQuest())
        {
            RewardManager.instance.GetRewardFromQuest(questReward);
        }
        PartyController.inventoryG.IncreaseCoin(questSetUp.GetCoinReward());
        var taskQuest = questSetUp.GetTask();
        taskQuest.SetComplete(true);    //true;
        taskQuest.SetLocked(false);         // true;
        taskQuest.SetReceived(true);         //false;
    }
    public void OnCancelQuest(QuestSetUp questSetUp)
    {
        currentQuest = Mathf.Clamp(currentQuest - 1, 0, maxQuest);
        var taskQuest = questSetUp.GetTask();
        taskQuest.SetComplete(false);
        taskQuest.SetLocked(false);     //false;
        taskQuest.SetReceived(false);
    }
    private void SortItemReward(List<ItemReward> itemRewards) => itemRewards.Sort((r1, r2) => r1.item.Rarity.CompareTo(r2.item.Rarity));
}
