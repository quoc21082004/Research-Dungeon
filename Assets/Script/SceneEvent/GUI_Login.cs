using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class GUI_Login : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI error_txt;
    [SerializeField] TMP_InputField loginEmailField;
    [SerializeField] TMP_InputField loginPassWordField;
    [SerializeField] Button startbtn;
    [SerializeField] Button quitbtn;

    private void OnEnable()
    {
        SetDefaultErrorText();
        SetDefaultLoginText();
        GUI_Input.playerInput.UI.OpenMenu.performed += OnEnterInput;
        GUI_Input.playerInput.UI.CloseMenu.performed += OnESCInput;
    }
    private void OnDisable()
    {
        GUI_Input.playerInput.UI.OpenMenu.performed -= OnEnterInput;
        GUI_Input.playerInput.UI.CloseMenu.performed -= OnESCInput;
    }
    private void OnEnterInput(InputAction.CallbackContext context)
    {
        Debug.Log("Enter");
        //startbtn.onClick?.Invoke();
    }
    private void OnESCInput(InputAction.CallbackContext context)
    {
        Debug.Log("Exit");
        //quitbtn.onClick?.Invoke();
    }
    public void SetErrorText(string _errorText)
    {
        error_txt.text = _errorText;
        Invoke(nameof(SetDefaultErrorText), 2.5f);
    }
    private void SetDefaultErrorText() => error_txt.text = "";
    private void SetDefaultLoginText()
    {
        loginEmailField.text = "";
        loginPassWordField.text = "";
        loginEmailField.Select();
    }

}