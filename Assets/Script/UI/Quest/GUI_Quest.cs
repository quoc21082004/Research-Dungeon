using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using PlayFab.EconomyModels;
public class GUI_Quest : MonoBehaviour
{
    [SerializeField] private GameObject questPanel;
    [SerializeField] private Animator noticeQuestanim;
    [SerializeField] private TextMeshProUGUI noticeQuest_txt;

    [SerializeField] private TextMeshProUGUI questTitle_txt;
    [SerializeField] private TextMeshProUGUI questDescription_txt;
    [SerializeField] private TextMeshProUGUI questInProgress_txt;
    [SerializeField] private TextMeshProUGUI questTaskNotice_txt;

    [SerializeField] private Button cancel_btn;
    [SerializeField] private Button accept_btn;
    [SerializeField] private Button report_btn;

    [SerializeField] public InventorySlot slotsprefab;

    [SerializeField] protected List<InventorySlot> itemReward;
    [SerializeField] private Transform contentItem;
    [SerializeField] private InventorySlot itemRequire;
    [SerializeField] private Transform contentItemRequire;

    [SerializeField] private List<QuestSetUp> quests;

    [SerializeField] private QuestBox questBoxprefab;
    [SerializeField] private List<QuestBox> questBox;
    [SerializeField] private Transform contentQuest;

    private QuestBox currentQuestBox;
    private bool canOpenPanel;
    private bool isAccept;
    private bool isReport;
    int indexItemNumber;

    private void OnEnable()
    {
        Init();
        RegisterEvent();
    }
    private void OnDisable()
    {
        UnRegisterEvent();
    }
    private void Start()
    {
        questBox.ForEach(_questbox => _questbox.OnQuestSelectEvent += OnSelectQuest);
    }
    private void OnDestroy()
    {
        questBox.ForEach(_questbox => _questbox.OnQuestSelectEvent -= OnSelectQuest);
    }
    private void Init()
    {
        questTitle_txt.text = "";
        questDescription_txt.text = "";
        canOpenPanel = true;
        questPanel.gameObject.SetActive(false);
        itemRequire.gameObject.SetActive(false);
    }
    private void RegisterEvent()
    {
        var gui = GUI_Input.playerInput;
        gui.UI.OpenQuest.performed += OnPanelOpenEvent;
        gui.UI.CloseQuest.performed += OnPanelCloseEvent;
        gui.Player.Disable();
        gui.PlayerAbility.Disable();
        accept_btn.onClick.AddListener(OnClickAcceptQuest);
        cancel_btn.onClick.AddListener(OnClickCancelQuest);
        report_btn.onClick.AddListener(OnClickReportQuest);
    }
    private void UnRegisterEvent()
    {
        var gui = GUI_Input.playerInput;
        gui.UI.OpenQuest.performed -= OnPanelOpenEvent;
        gui.UI.CloseQuest.performed -= OnPanelOpenEvent;
        gui.Player.Enable();
        gui.PlayerAbility.Enable();
        accept_btn.onClick.RemoveListener(OnClickAcceptQuest);
        cancel_btn.onClick.RemoveListener(OnClickCancelQuest);
        report_btn.onClick.RemoveListener(OnClickReportQuest);
    }
    private void OnPanelOpenEvent(InputAction.CallbackContext context)
    {
        if (!canOpenPanel)
            return;
        questPanel.gameObject.SetActive(true);
        GUI_Input.playerInput.UI.OpenShop.Disable();
        GUI_Input.playerInput.UI.OpenMap.Disable();
        ShowQuest();
    }
    private void OnPanelCloseEvent(InputAction.CallbackContext context)
    {

        GUI_Input.playerInput.UI.OpenShop.Enable();
        GUI_Input.playerInput.UI.OpenMap.Enable();
    }
    #region            Handle Accept - Cancel - Report

    private void OnClickAcceptQuest()
    {
        isAccept = true;
        OpenNoticeQuestPanel("Would you like to accept this quest?\nContinue?");
    }
    private void OnClickReportQuest()
    {
        isReport = true;
        OpenNoticeQuestPanel("Would you like to report this mission?\nTo Complete , you will lost item require to get reward\nContinue?");
    }
    private void OnClickCancelQuest()
    {
        isAccept = false;
        OpenNoticeQuestPanel("Would you like to quit this mission?\nContinue?");
    }
    public void OnClickConfirmBtn()
    {
        if (isReport) // complete quest
        {
            isReport = false;
            var taskRequirement = currentQuestBox.questSetUp.requireQuest;
            PartyController.inventoryG.items[indexItemNumber].currentAmt -= taskRequirement.amount;
            QuestManager.instance.OnCompleteQuest(currentQuestBox.questSetUp);
        }
        else if (isAccept) // accept quest
        {
            isAccept = false;
            currentQuestBox.SetReceiveQuestBox(true);
            QuestManager.instance.OnStartQuest(currentQuestBox.questSetUp);
        }
        else if (!isAccept) // not yet done quest
        {
            currentQuestBox.SetReceiveQuestBox(false);
            currentQuestBox.LockTask();
            currentQuestBox.isLocked = true;
            QuestManager.instance.OnCancelQuest(currentQuestBox.questSetUp);
        }
        OnClickCancelBtn();
        SetProgressQuest();     // 0   /  3
        SetButton(currentQuestBox);
        CheckQuestReport(currentQuestBox);
        SetNoticeText(currentQuestBox.questSetUp.taskQuest);
    }
    private void OnClickCancelBtn()
    {
        isReport = false;
        noticeQuestanim.Play("NoticeQuest_Close");
    }
    public void OnSelectQuest(QuestBox _questbox)
    {
        currentQuestBox = _questbox;
        var _questSetUp = _questbox.questSetUp;
        questTitle_txt.text = _questSetUp.titleQuest;
        questDescription_txt.text = _questSetUp.descriptionQuest;

        SpawnItemReward(_questSetUp);
        SetItemRequire(_questSetUp);
        SetButton(_questbox);
        CheckQuestReport(_questbox);
        SetNoticeText(_questSetUp.taskQuest);
    }
    #endregion 
    private void ShowQuest()
    {
        questTitle_txt.text = "???";
        questDescription_txt.text = "???";
        questTaskNotice_txt.text = "";
        itemRequire.gameObject.SetActive(false);
        // create quest box
        for (int i = 0; i < quests.Count; i++)
        {
            PoolManager.instance.Release(questBoxprefab.gameObject, contentQuest.position);
        }
        questBox = contentQuest.GetComponentsInChildren<QuestBox>().ToList();
        int count = 0;
        var boxquestActive = questBox.Where(_questbox => _questbox.gameObject.activeSelf).ToList();
        foreach (var _questbox in boxquestActive)
        {
            _questbox.SetUpQuestBox(quests[count]);
            CheckQuestReport(_questbox);
            count++;
        }
        SetProgressQuest();
    }
    private void OpenNoticeQuestPanel(string _txt)
    {
        noticeQuestanim.Play("NoticeQuest_Open");
        noticeQuest_txt.text = "" + _txt;
    }
    private void SpawnItemReward(QuestSetUp _questSetUp)
    {
        var reward = _questSetUp.rewardQuest;
        for (int i = 0; i < reward.Count; i++)
        {
            PoolManager.instance.Release(slotsprefab.gameObject, contentItem.position);
        }
        itemReward = contentItem.GetComponentsInChildren<InventorySlot>().ToList();

        PoolManager.instance.Release(slotsprefab.gameObject, contentItemRequire.position);
        itemRequire = contentItemRequire.GetComponentInChildren<InventorySlot>();
      
        int count = 0;
        var _items = itemReward.Where(items => items.gameObject.activeSelf).ToList();

        foreach (var rewardSlotItem in _items)
        {
            var itemSO = reward[count].item;
            var itemValue = reward[count].GetValueRandom();     // count item

            rewardSlotItem.AddItem(itemSO, itemValue);
            rewardSlotItem.SetAmountText(itemValue.ToString());
            count++;
        }
    }
    private void SetItemRequire(QuestSetUp _questSetUp)
    {
        itemRequire.gameObject.SetActive(true);
        var taskRequire = _questSetUp.requireQuest;
        var taskRequireItem = taskRequire.requireItem;
        var hasItem = PartyController.inventoryG.items[taskRequireItem.itemNumber].currentAmt;
        itemRequire.AddItem(taskRequireItem, taskRequire.amount);
        itemRequire.SetAmountText("" + hasItem.ToString() + "/" + taskRequire.amount);
    }
    private void SetButton(QuestBox _questbox)
    {
        accept_btn.gameObject.SetActive(!_questbox.isReceived);
        report_btn.gameObject.SetActive(_questbox.isReceived);

        var checkCommon = !_questbox.isLocked && !_questbox.isComplete;
        accept_btn.interactable = checkCommon && !_questbox.isReceived && QuestManager.instance.currentQuest < QuestManager.instance.maxQuest;
        cancel_btn.interactable = checkCommon && _questbox.isReceived;
    }
    private void CheckQuestReport(QuestBox _questbox)
    {
        var taskRequire = _questbox.questSetUp.requireQuest;        // _taskRequired.GetValue() <= _userData.HasItemValue(_taskRequired.GetNameCode());
        ItemSO require_item = new ItemSO();
        foreach (var searchitem in PartyController.inventoryG.items)
        {
            if (taskRequire.requireItem.itemNumber == searchitem.itemNumber)
            {
                indexItemNumber = searchitem.itemNumber;
                require_item = searchitem;
            }
        }
        var checkComplete = taskRequire.amount <= require_item.currentAmt;
        report_btn.interactable = checkComplete && !_questbox.questSetUp.taskQuest.isCompleted;
        _questbox.SetReportQuestBox(report_btn.interactable);
    }
    private void SetProgressQuest() => questInProgress_txt.text = "In Progress :" + QuestManager.instance.currentQuest + " / " + QuestManager.instance.maxQuest;
    private void SetNoticeText(Task task) => questTaskNotice_txt.text = task.isCompleted ? "You have completed this task" : task.isLocked ? "Can't handle task today" : "";

}
