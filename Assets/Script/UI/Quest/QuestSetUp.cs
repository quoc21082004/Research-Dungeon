using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
[System.Serializable]
public class Task
{
    [SerializeField] public bool isCompleted;
    [SerializeField] public bool isLocked;
    [SerializeField] public bool isReceived;
    Task() { }
    Task(bool _isComplete, bool _isLocked, bool _isReceived)
    {
        isCompleted = _isComplete;
        isLocked = _isLocked;
        isReceived = _isReceived;
    }
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
    public Task taskQuest;
    public string titleQuest;
    public string descriptionQuest;
    public TaskRequirement requireQuest;
    public List<ItemReward> rewardQuest;
}
