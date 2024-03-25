using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FullMapManager : Singleton<FullMapManager>
{
    public GameObject mapUI;
    bool isMapOpen = false;
    private void OnEnable()
    {
        var guiInput = GUI_Input.playerInput.UI;
        guiInput.OpenMap.performed += OpenMap;
    }
    private void OnDisable()
    {
        var guiInput = GUI_Input.playerInput.UI;
        guiInput.OpenMap.performed -= OpenMap;
    }
    private void OpenMap(InputAction.CallbackContext context)
    {
        if (!isMapOpen)
            FullMapManager.instance.Map();
        else if (isMapOpen)
            FullMapManager.instance.CloseMap();
    }
    public void Map()
    {
        isMapOpen = true;
        mapUI.SetActive(true);
        InputManager.playerInput.Disable();
        GUI_Input.playerInput.UI.OpenShop.Disable();
        GUI_Input.playerInput.UI.OpenQuest.Disable();
    }
    public void CloseMap()
    {
        mapUI.SetActive(false);
        InputManager.playerInput.Enable();
        GUI_Input.playerInput.UI.OpenShop.Enable();
        GUI_Input.playerInput.UI.OpenQuest.Enable();
        StartCoroutine(ResumeNextFrame());
    }
    IEnumerator ResumeNextFrame()
    {
        yield return null;
        isMapOpen = false;
    }
}
