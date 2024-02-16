using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GUI_Input : MonoBehaviour
{
    public static Inputsystem playerInput;
    private void Awake()
    {
        playerInput = new Inputsystem();
    }
    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
    public static void EnableInput() => playerInput.Enable();
    public static void DisableInput() => playerInput.Disable();
}
