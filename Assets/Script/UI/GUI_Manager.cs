using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GUI_Manager
{
    private static readonly List<IGUI> GUIDatas = new();

    public static void SendReference(GameManager _gameManager)
    {
        GUIDatas.ForEach(item => item.GetReference(_gameManager));
        Debug.Log("Send Reference");
    }
    public static void UpdateData()
    {
        GUIDatas.ForEach(gui => gui.UpdateDataGUI());
        Debug.Log("update data GUI");
    }

    public static void AddGUI(IGUI iGui)
    {
        if (GUIDatas.Contains(iGui))
            return;
        GUIDatas.Add(iGui);
    }
    public static void RemoveGUI(IGUI iGui)
    {
        if (!GUIDatas.Contains(iGui))
            return;
        GUIDatas.Remove(iGui);
    }
}
