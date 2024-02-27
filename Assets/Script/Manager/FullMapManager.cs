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
        {
            if (!PauseMenu.isGamePause)
                FullMapManager.instance.Map();
        }
        else if (isMapOpen)
        {
            if (PauseMenu.isGamePause)
                FullMapManager.instance.CloseMap();
        }
    }
    public void Map()
    {
        isMapOpen = true;
        mapUI.SetActive(true);
        PauseMenu.instance.Pause();
    }
    public void CloseMap()
    {
        mapUI.SetActive(false);
        StartCoroutine(ResumeNextFrame());
    }
    IEnumerator ResumeNextFrame()
    {
        yield return null;
        isMapOpen = false;
        PauseMenu.instance.Resume();
    }
}
