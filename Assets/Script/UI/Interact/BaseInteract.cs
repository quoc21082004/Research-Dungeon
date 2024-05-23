using System.Drawing;
using UnityEngine;
public class BaseInteract : MonoBehaviour
{
    [SerializeField][Range(0.75f, 1.5f)] private float radius;
    public LayerMask layerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteract interact = collision.gameObject.GetComponent<IInteract>();
        if (interact != null)
        {
            DialogueManager.instance.interactUI.OnEnterPlayer();
            DialogueManager.instance.interactUI.OnPanelOpenEvent += Interact;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteract interact = collision.gameObject.GetComponent<IInteract>();
        if (interact != null)
        {
            DialogueManager.instance.interactUI.OnExitPlayer();
            DialogueManager.instance.interactUI.OnPanelOpenEvent -= Interact;
        }
    }
    private void Interact()
    {
        var collider = Physics2D.OverlapCircleAll(transform.position, radius, LayerMaskHelper.layerMaskInteract);
        if (collider.Length == 0)
            return;
        foreach(var colli in collider)
        {
            IInteract interact = colli.gameObject.GetComponent<IInteract>();
            if (interact != null)
                interact.Interact();
        }
    }
}
