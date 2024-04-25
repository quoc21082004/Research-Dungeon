using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class GUI_Quest : MonoBehaviour , IGUI
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

    [SerializeField] private InventorySlot itemrequire;
    [SerializeField] private List<InventorySlot> itemReward;
    [SerializeField] TextMeshProUGUI coinReward_txt;

    [SerializeField] private Transform contentItem;
    [SerializeField] private Transform contentItemRequire;

    [SerializeField] private List<QuestSetUp> quests => QuestManager.instance.questList;

    [SerializeField] private QuestBox questBoxprefab;
    [SerializeField] private List<QuestBox> questBox;
    [SerializeField] private Transform contentQuest;

    private QuestBox currentQuestBox;
    private bool canOpenPanel;
    private bool isAccept;
    private bool isReport;

    private void Start()
    {
        foreach (var _questbox in questBox)
            _questbox.OnQuestSelectEvent -= OnSelectQuest;
    }
    private void OnDestroy()
    {
        foreach (var _questbox in questBox)
            _questbox.OnQuestSelectEvent -= OnSelectQuest;
    }
    private void OnEnable()
    {
        Init();
        RegisterEvent();
    }
    private void OnDisable() => UnRegisterEvent();
    private void Init()
    {
        questTitle_txt.text = "";
        questDescription_txt.text = "";
        coinReward_txt.text = "";
        canOpenPanel = true;
        questPanel.gameObject.SetActive(false);
    }
    public void GetReference(GameManager _gameManager) { }
    public void UpdateDataGUI() { }
    private void RegisterEvent()
    {
        var gui = GUI_Input.playerInput;
        gui.UI.OpenQuest.performed += OnPanelOpenEvent;
        gui.UI.CloseQuest.performed += OnPanelCloseEvent;

        accept_btn.onClick.AddListener(OnClickAcceptQuest);
        cancel_btn.onClick.AddListener(OnClickCancelQuest);
        report_btn.onClick.AddListener(OnClickReportQuest);
    }
    private void UnRegisterEvent()
    {
        var gui = GUI_Input.playerInput;
        gui.UI.OpenQuest.performed -= OnPanelOpenEvent;
        gui.UI.CloseQuest.performed -= OnPanelCloseEvent;

        accept_btn.onClick.RemoveListener(OnClickAcceptQuest);
        cancel_btn.onClick.RemoveListener(OnClickCancelQuest);
        report_btn.onClick.RemoveListener(OnClickReportQuest);
    }
    public void OnPanelOpenEvent(InputAction.CallbackContext context)
    {
        if (!canOpenPanel)
            return;
        foreach (var _questbox in questBox)
            _questbox.OnQuestSelectEvent += OnSelectQuest;
        canOpenPanel = false;
        questPanel.gameObject.SetActive(true);
        GUI_Input.playerInput.UI.OpenShop.Disable();
        InputManager.playerInput.Disable();
        ShowQuest();
    }
    public void OpenQuestPanel() => OnPanelOpenEvent(default);
    public void CloseQuestPanel() => OnPanelCloseEvent(default);
    public void OnPanelCloseEvent(InputAction.CallbackContext context)
    {
        if (canOpenPanel)
            return;
        canOpenPanel = true;
        questPanel.gameObject.SetActive(false);
        InputManager.playerInput.Enable();
        GUI_Input.playerInput.UI.OpenShop.Enable();
    }
    public void PanelCloseX()
    {
        if (canOpenPanel)
            return;
        canOpenPanel = true;
        questPanel.gameObject.SetActive(false);
        InputManager.playerInput.Enable();
        GUI_Input.playerInput.UI.OpenShop.Enable();
    }

    #region            Handle Accept - Cancel - Report

    private void OnClickAcceptQuest()
    {
        isAccept = true;
        OpenNoticeQuestPanel("\n\nWould you like to accept this quest!\nContinue?");
    }
    private void OnClickReportQuest()
    {
        isReport = true;
        OpenNoticeQuestPanel("\n\nWould you like to report this mission!\nTo Complete , you will lost item require to get reward\nContinue?");
    }
    private void OnClickCancelQuest()
    {
        isAccept = false;
        OpenNoticeQuestPanel("\n\nWould you like to quit this mission!\nContinue?");
    }
    public void OnClickConfirmBtn()
    {
        if (isReport) // complete quest
        {
            isReport = false;
            var taskRequirement = currentQuestBox.questSetUp.requireQuest;
            PartyController.inventoryG.Remove(taskRequirement.requireItem, taskRequirement.amount);
            QuestManager.instance.OnCompleteQuest(currentQuestBox.questSetUp);
            OnSelectQuest(currentQuestBox);
        }
        else if (isAccept) // accept quest
        {
            isAccept = false;
            currentQuestBox.SetReceiveQuestBox(true);
            QuestManager.instance.OnStartQuest(currentQuestBox.questSetUp);
        }
        else  // cancel quest
        {
            currentQuestBox.SetReceiveQuestBox(false);
            QuestManager.instance.OnCancelQuest(currentQuestBox.questSetUp);
        }
        OnClickCancelBtn();
        SetProgressQuest();    
        SetButton(currentQuestBox);
        CheckQuestReport(currentQuestBox);
        SetNoticeQuestText(currentQuestBox.questSetUp.taskQuest);
    }
    public void OnClickCancelBtn()
    {
        isReport = false;
        noticeQuestanim.Play("NoticeQuest_Close");
    }
    private void OpenNoticeQuestPanel(string _txt)
    {
        noticeQuestanim.Play("NoticeQuest_Open");
        noticeQuest_txt.text = "" + _txt;
    }
    public void OnSelectQuest(QuestBox _questbox)
    {
        ShowQuest();
        currentQuestBox = _questbox;
        var _questSetUp = _questbox.questSetUp;
        questTitle_txt.text = _questSetUp.titleQuest;
        questDescription_txt.text = _questSetUp.descriptionQuest;

        CheckCompleteColor(_questbox);
        SpawnItemReward(_questSetUp);
        SpawnItemRequire(_questSetUp);
        SetButton(_questbox);
        CheckQuestReport(_questbox);
        SetNoticeQuestText(_questSetUp.taskQuest);
    }
    void CheckCompleteColor(QuestBox _questbox)
    {
        if (_questbox.isComplete)
            _questbox.accept_icon.color = Color.white;
        else
            _questbox.accept_icon.color = Color.red;
    }
    #endregion 
    private void ShowQuest()
    {
        questTitle_txt.text = "???";
        questDescription_txt.text = "???";
        questTaskNotice_txt.text = "";
        coinReward_txt.text = "";

        questBox.ForEach(_questbox => _questbox.gameObject.SetActive(false));
        itemReward.ForEach(_itemreward => _itemreward.gameObject.SetActive(false));
        itemrequire.gameObject.SetActive(false);

        for (int i = 0; i < quests.Count; i++)
        {
            var questbox = PoolManager.instance.Release(questBoxprefab.gameObject);
            questbox.transform.SetParent(contentQuest);
        }
        questBox = contentQuest.GetComponentsInChildren<QuestBox>().ToList();
        int count = 0;
        foreach (var _questbox in questBox)
        {
            _questbox.SetUpQuestBox(quests[count]);
            CheckQuestReport(_questbox);
            count++;
        }
        SetProgressQuest();
    }
    private void SpawnItemReward(QuestSetUp _questSetUp)
    {
        var reward = _questSetUp.rewardQuest;
        for (int i = 0; i < reward.Count; i++)
        {
            var slots = PoolManager.instance.Release(slotsprefab.gameObject);
            slots.transform.SetParent(contentItem);
            slots.GetComponentInChildren<Button>().enabled = false;
            slots.GetComponentInChildren<InventorySlotBtn>().enabled = false;
            slots.GetComponentInChildren<ClickItemOption>().enabled = false;
        }
        itemReward = contentItem.GetComponentsInChildren<InventorySlot>().ToList();

        int count = 0;
        var _items = itemReward.Where(items => items.gameObject.activeSelf).ToList();
        coinReward_txt.text = _questSetUp.coinReward.ToString() + " <sprite=3>";
        foreach (var rewardSlotItem in _items)
        {
            var itemSO = reward[count].item;
            var itemValue = reward[count].value;     // count item
            rewardSlotItem.AddItem(itemSO, itemValue);
            rewardSlotItem.SetAmountText(itemValue.ToString());
            count++;
        }
    }
    private void SpawnItemRequire(QuestSetUp _questSetUp)
    {
        itemrequire.gameObject.SetActive(true);
        itemrequire.GetComponentInChildren<InventorySlotBtn>().enabled = false;
        itemrequire.GetComponentInChildren<ClickItemOption>().enabled = false;
        itemrequire.GetComponentInChildren<Button>().enabled = false;

        var taskRequire = _questSetUp.requireQuest;
        var taskRequireItem = taskRequire.requireItem;
        var taskRequireItemNumber = taskRequireItem.itemNumber;
        int hasItem = PartyController.inventoryG.GetItemAmt(taskRequireItem);
        if (hasItem < taskRequire.amount)
            itemrequire.stackItem_text.color = Color.red;
        else if (hasItem >= taskRequire.amount) 
            itemrequire.stackItem_text.color = Color.white;
        itemrequire.AddItem(taskRequireItem, taskRequire.amount);
        itemrequire.SetAmountText("" + hasItem.ToString() + "/" + taskRequire.amount);
    }
    private void SetButton(QuestBox _questbox)
    {
        accept_btn.gameObject.SetActive(!_questbox.isReceived);
        report_btn.gameObject.SetActive(_questbox.isReceived);
        bool checkCommon = !_questbox.isLocked && !_questbox.isComplete;
        accept_btn.interactable = checkCommon && !_questbox.isReceived && QuestManager.instance.currentQuest < QuestManager.instance.maxQuest;
        cancel_btn.interactable = checkCommon && _questbox.isReceived;
    }
    private void CheckQuestReport(QuestBox _questbox)
    {
        var taskRequire = _questbox.questSetUp.requireQuest;
        int requireItemAmount = PartyController.inventoryG.GetItemAmt(taskRequire.requireItem);
        var checkComplete = taskRequire.amount <= requireItemAmount;
        report_btn.interactable = checkComplete && !_questbox.questSetUp.taskQuest.isCompleted;
        _questbox.SetReportQuestBox(report_btn.interactable);
    }
    private void SetProgressQuest() => questInProgress_txt.text = "In Progress :" + QuestManager.instance.currentQuest + " / " + QuestManager.instance.maxQuest;
    private void SetNoticeQuestText(Task task) => questTaskNotice_txt.text = task.isCompleted ? "You have completed this mission" : task.isLocked ? "You can't handle this mission" : "";
}
