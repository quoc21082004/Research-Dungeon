using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    public static Inputsystem playerInput;
    protected override void Awake()
    {
        base.Awake();
        playerInput = new Inputsystem();
    }
    private void OnEnable() => playerInput.Enable();
    private void OnDisable() => playerInput.Disable();
    public static void EnableInput() => playerInput.Enable();
    public static void DisableInput() => playerInput.Disable();
    public void DisableActionFor(InputAction action, float delayTime)
    {
        StartCoroutine(DisableActionCoroutine(action, delayTime));
    }
    private IEnumerator DisableActionCoroutine(InputAction action, float delayTime)
    {
        action.Disable();
        yield return new WaitForSeconds(delayTime);
        action.Enable();
    }
}