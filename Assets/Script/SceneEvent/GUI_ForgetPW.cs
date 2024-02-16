using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class GUI_ForgetPW : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI error_txt;
    [SerializeField] TMP_InputField emailField;
    [SerializeField] Button veficatatebtn;
    [SerializeField] Button quitbtn;

    private void OnEnable()
    {
        SetDefaultTextError();
        SetDefaultFieldText();

        GUI_Input.playerInput.UI.OpenMenu.performed += OnEnterInput;
        GUI_Input.playerInput.UI.CloseMenu.performed += OnESCInput;
    }
    private void OnDisable()
    {
        GUI_Input.playerInput.UI.OpenMenu.performed -= OnEnterInput;
        GUI_Input.playerInput.UI.CloseMenu.performed -= OnESCInput;
    }
    private void OnESCInput(InputAction.CallbackContext context)
    {
        veficatatebtn.onClick?.Invoke();
    }
    private void OnEnterInput(InputAction.CallbackContext context)
    {
        quitbtn.onClick?.Invoke();
    }
    private void SetErrorText(string _error)
    {
        error_txt.text = _error;
        Invoke(nameof(SetDefaultTextError), 2.5f);
    }
    public void SetDefaultTextError() => error_txt.text = "";
    public void SetDefaultFieldText()
    {
        emailField.text = "";
        emailField.Select();
    }
}