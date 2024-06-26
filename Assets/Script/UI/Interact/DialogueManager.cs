using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : Singleton<DialogueManager>, IPointerClickHandler
{
    [SerializeField] public InteractiveUI interactUI;
    public bool IsUsing = false;
    private Action<DialogueObject> showDialogueAction;
    [SerializeField] GameObject dialogue;
    [SerializeField] TextMeshProUGUI dialogue_txt;
    [SerializeField] TextMeshProUGUI name_txt;
    private bool isWriting = false;
    private Coroutine writeCourtine;
    private Coroutine clickStateCourtine;
    private bool wasPointerClick;
    [SerializeField] private OptionBox optionBox;
    float writeSpeed;
    bool isSkipDialogue;
    protected override void Awake()
    {
        base.Awake();
        showDialogueAction = ShowDialogueInternal;
        writeSpeed = Time.fixedDeltaTime;
    }
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        NoticeManager.instance.CloseNoticeInteract();
        IsUsing = true;
        if (showDialogueAction == null)
        {
            var dialoguePanel = FindAnyObjectByType<DialogueManager>();
            if (dialoguePanel != null)
            {
                showDialogueAction = dialoguePanel.ShowDialogueInternal;
                showDialogueAction.Invoke(dialogueObject);
            }
        }
        else
            showDialogueAction.Invoke(dialogueObject);
    }
    private void ShowDialogueInternal(DialogueObject dialogueObject)
    {
        dialogue.gameObject.SetActive(true);
        InputManager.playerInput.Disable();  
        GUI_Input.playerInput.Disable();
        wasPointerClick = false;
        StopAllCoroutines();
        StartCoroutine(ShowDialogueCoroutine(dialogueObject));
    }
    private IEnumerator ShowDialogueCoroutine(DialogueObject dialogueObject)
    {
        name_txt.text = dialogueObject.name.ToString();
        foreach (var sentence in dialogueObject.sentences)
        {
            StartWrite(sentence);
            yield return new WaitWhile(() => isWriting && !wasPointerClick);
            StopWrite();
            dialogue_txt.color = Color.green;
            yield return null;
            yield return new WaitUntil(() => wasPointerClick);
            yield return null;
        }
        if (dialogueObject.options.Count > 0)
        {
            optionBox.ShowOption(dialogueObject);
            yield return new WaitWhile(() => optionBox.isShowing);
        }
        CompleteDialogue();
    }
    private void CompleteDialogue()
    {
        StopAllCoroutines();
        dialogue.gameObject.SetActive(false);
        InputManager.playerInput.Enable();
        GUI_Input.playerInput.Enable();
        IsUsing = false;
    }

    private IEnumerator TypeWritterCoroutine(string sentence)
    {
        isWriting = true;
        dialogue_txt.text = "";
        foreach (var letter in sentence.ToCharArray())
        {
            writeSpeed = isSkipDialogue ? 0f : Time.fixedDeltaTime;
            dialogue_txt.text += letter;
            yield return new WaitForSeconds(writeSpeed);
        }
        isWriting = false;
        isSkipDialogue = false;
    }
    private void StartWrite(string sentence) => writeCourtine = StartCoroutine(TypeWritterCoroutine(sentence));
    private void StopWrite()
    {
        if (writeCourtine != null)
        {
            StopCoroutine(writeCourtine);
            writeCourtine = null;
        }
        isWriting = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickStateCourtine != null)
        {
            StopCoroutine(clickStateCourtine);
            clickStateCourtine = null;
        }
        isSkipDialogue = true;
        wasPointerClick = true;
        clickStateCourtine = StartCoroutine(ClearClickState());
    }

    private IEnumerator ClearClickState()
    {
        yield return null;
        wasPointerClick = false;
    }
}
