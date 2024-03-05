using System.Collections;
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
    public bool IsCompleted() => isCompleted;
    public bool IsLocked() => isLocked;
    public bool IsReceived() => isReceived;
    public void SetTaskComplete(bool _value) => isCompleted = _value;
    public void SetTaskLocked(bool _value) => isLocked = _value;
    public void SetTaskReceived(bool _value) => isReceived = _value;
}
[System.Serializable]
public class TaskRequirement
{
    [SerializeField] private ItemSO requireItem; // require item for task
    [SerializeField] private int value;
    public ItemSO GetItem() => requireItem;
    public void SetItem(ItemSO _value) => requireItem = _value;
    public int GetValue() => value;
    public void SetValue(int _value) => value = _value;
}
[CreateAssetMenu(fileName = "Quest", menuName = "QuestSO")] 
public class QuestSetUp : ScriptableObject
{
    [SerializeField] private Task taskQuest;
    [SerializeField] private string titleQuest;
    [SerializeField] private string descriptionQuest;
    [SerializeField] private TaskRequirement requireQuest;
    [SerializeField] private List<ItemReward> rewardQuest;
}
