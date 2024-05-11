using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractiveUI : MonoBehaviour
{
    [SerializeField] string noticePlayerText { get; set; }   // active when have collision to player

    public event Action OnPanelOpenEvent;
    public void OnEnterPlayer()
    {
        GUI_Input.playerInput.UI.Enable();
        GUI_Input.playerInput.UI.OpenInteract.started += OpenInteract;
        NoticeManager.instance.CreateNoticeInteract(noticePlayerText);
    }
    public void OnExitPlayer()
    {
        GUI_Input.playerInput.UI.Disable();
        GUI_Input.playerInput.UI.OpenInteract.canceled -= OpenInteract;
        NoticeManager.instance.CloseNoticeInteract();
    }
    private void OpenInteract(InputAction.CallbackContext context) => OnPanelOpenEvent?.Invoke();
}
