using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Task
{
    [SerializeField] private bool isCompleted;
    [SerializeField] private bool isLocked;
    [SerializeField] private bool isReceived;
    Task() { }
    Task(bool _isComplete, bool _isLocked, bool _isReceived)
    {
        isCompleted = _isComplete;
        isLocked = _isLocked;
        isReceived = _isReceived;
    }
    public bool IsComplete() => isCompleted;
    public void SetComplete(bool _value) => isCompleted = _value;
    public bool IsLocked() => isLocked;
    public void SetLocked(bool _value) => isLocked = _value;
    public bool IsReceived() => isReceived;
    public void SetReceived(bool _value) => isReceived = _value;
}
[System.Serializable]
public class TaskRequirement  
{
    [SerializeField] public ItemSO requireItem; // require item for task

    [SerializeField] public int amount;
    public ItemSO GetItemSO() => requireItem;
    public int GetValue() => amount;
}
[CreateAssetMenu(fileName = "Quest", menuName = "QuestSO")] 
public class QuestSetUp : ScriptableObject
{
    [SerializeField] private Task taskQuest;
    [SerializeField] private string titleQuest;
    [SerializeField] private string descriptionQuest;
    [SerializeField] private TaskRequirement requireQuest;
    [SerializeField] private List<ItemReward> rewardQuest;
    [SerializeField] private int coinReward;

    public Task GetTask() => taskQuest;
    public string GetTitleQuest() => titleQuest;
    public void SetTitleQuest(string _value) => titleQuest = _value;
    public string GetDescriptionQuest() => descriptionQuest;
    public TaskRequirement GetRequireQuest()=> requireQuest;
    public List<ItemReward> GetRewardQuest() => rewardQuest;
    public int GetCoinReward() => coinReward;
}
