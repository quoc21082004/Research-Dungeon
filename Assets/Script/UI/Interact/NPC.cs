using UnityEngine;
public class NPC : BaseInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    public override void Interact()
    {
        AudioManager.instance.PlaySfx("Click");
        DialogueManager.instance.ShowDialogue(dialogueObject);
    }
}
