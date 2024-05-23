using UnityEngine;
public class NPC : MonoBehaviour , IInteract
{
    [SerializeField] private DialogueObject dialogueObject;

    public void Interact()
    {
        AudioManager.instance.PlaySfx("Click");
        DialogueManager.instance.ShowDialogue(dialogueObject);
    }
}
