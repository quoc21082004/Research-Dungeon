using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public abstract class BaseInteractable : MonoBehaviour, IInteract
{
    private void OnEnable() => DialogueManager.instance.interactUI.OnPanelOpenEvent += Interact;
    private void OnDisable() => DialogueManager.instance.interactUI.OnPanelOpenEvent -= Interact;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerCTL>(out var player))
        {
            DialogueManager.instance.interactUI.OnPanelOpenEvent += Interact;
            DialogueManager.instance.interactUI.OnEnterPlayer();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerCTL>(out var player))
            DialogueManager.instance.interactUI.OnExitPlayer();
    }
    public abstract void Interact();
}
