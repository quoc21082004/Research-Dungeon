using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractiveUI : MonoBehaviour
{
    [SerializeField] private string noticePlayerText;   // active when have collision to player

    public event Action OnPanelOpenEvent;
    public void OnEnterPlayer()
    {
        GUI_Input.playerInput.UI.OpenInteract.performed += OpenInteract;
        NoticeManager.instance.CreateNoticeInteract(noticePlayerText);
    }
    public void OnExitPlayer()
    {
        GUI_Input.playerInput.UI.OpenInteract.performed -= OpenInteract;
        NoticeManager.instance.CloseNoticeInteract();
    }
    private void OpenInteract(InputAction.CallbackContext context) => OnPanelOpenEvent?.Invoke();
}
