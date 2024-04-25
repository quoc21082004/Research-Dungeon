using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class MenuController : Singleton<MenuController>
{
    public GameObject bag_panel;
    bool isOpenBag = false;
    GameManager _gameManager;
    PlayerSO playerdata;
    private void Start() => isOpenBag = false;
    private void OnEnable() => RegisterEvent();
    private void OnDisable() => UnRegisterEvent();
    private void RegisterEvent()
    {
        GUI_Input.playerInput.UI.OpenBag.performed += OpenBag;
        GUI_Input.playerInput.UI.CloseBag.performed += CloseBag;
        _gameManager = GameManager.instance;

        GUI_Manager.SendReference(_gameManager);
        GUI_Manager.UpdateData();
    }
    private void UnRegisterEvent()
    {
        GUI_Input.playerInput.UI.OpenBag.performed -= OpenBag;
        GUI_Input.playerInput.UI.CloseBag.performed -= CloseBag;
    }
    private void OpenBag(InputAction.CallbackContext context)
    {
        if (isOpenBag)
            return;
        isOpenBag = true;
        Open();
    }
    private void CloseBag(InputAction.CallbackContext context)
    {
        if (!isOpenBag)
            return;
        isOpenBag = false;
        Close();
    }
    public void Open()
    {
        GUI_Manager.UpdateData();
        bag_panel.gameObject.SetActive(true);
        Time.timeScale = 0;
        InputManager.playerInput.Disable();
    }
    public void Close()
    {
        bag_panel.gameObject.SetActive(false);
        isOpenBag = false;
        Time.timeScale = 1;
        InputManager.playerInput.Enable();
    }

}
