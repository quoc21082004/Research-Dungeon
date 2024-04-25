using UnityEngine;
using UnityEngine.Rendering;
public class NPC : BaseInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    private void OnEnable() => DialogueManager.instance.interactUI.OnPanelOpenEvent += Interact;
    private void OnDisable() => DialogueManager.instance.interactUI.OnPanelOpenEvent -= Interact;
    public override void Interact() => DialogueManager.instance.ShowDialogue(dialogueObject);
}
