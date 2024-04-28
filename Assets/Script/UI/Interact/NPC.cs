using UnityEngine;
using UnityEngine.Rendering;
public class NPC : BaseInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    public override void Interact() => DialogueManager.instance.ShowDialogue(dialogueObject);
}
