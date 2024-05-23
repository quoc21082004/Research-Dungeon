
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GUI_Input : Singleton<GUI_Input>
{
    public static Inputsystem playerInput;
    protected override void Awake()
    {
        playerInput = new Inputsystem();
    }
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.UI.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.UI.Disable();
    }
    public static void EnableInput() => playerInput.Enable();
    public static void DisableInput() => playerInput.Disable();
}
