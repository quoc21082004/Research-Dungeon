using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleQuest_txt;
    [SerializeField] public Image accept_icon;
    [SerializeField] public Image report_icon;
    public event Action<QuestBox> OnQuestSelectEvent;

    public QuestSetUp questSetUp;       // quest SO
    //      Mood Of Quest
    public bool isReceived;
    public bool isComplete;
    public bool isLocked;
    public void SetUpQuestBox(QuestSetUp _questSetUp)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);

        questSetUp = _questSetUp;
        titleQuest_txt.text = _questSetUp.titleQuest;

        report_icon.enabled = false;

        Task questTask = questSetUp.taskQuest;
        isReceived = questTask.isReceived;
        isComplete = questTask.isCompleted;
        isLocked = questTask.isLocked;

        SetReceiveQuestBox(isReceived && !isLocked);
    }
    public void SelectQuestBox() => OnQuestSelectEvent?.Invoke(this);
    public void SetReceiveQuestBox(bool _isReceived)
    {
        accept_icon.enabled = _isReceived;
        isReceived = _isReceived;
    }
    public void SetReportQuestBox(bool _canComplete) => report_icon.enabled = (_canComplete && isReceived);
    public void LockTask() => isLocked = true;
}