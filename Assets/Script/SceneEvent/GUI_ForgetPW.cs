using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class GUI_ForgetPW : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI error_txt;
    [SerializeField] public TMP_InputField emailField;
    [SerializeField] public Button veficatate_btn;
    [SerializeField] public Button quit_btn;

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
    private void OnEnterInput(InputAction.CallbackContext context)
    {
        veficatate_btn.onClick?.Invoke();
    }
    private void OnESCInput(InputAction.CallbackContext context)
    {
        quit_btn.onClick?.Invoke();
    }
    public void SetErrorText(string _error)
    {
        error_txt.text = _error;
        Invoke(nameof(SetDefaultTextError), 6f);
    }
    public void SetDefaultTextError() => error_txt.text = "";
    public void SetDefaultFieldText()
    {
        emailField.text = "";
        emailField.Select();
    }
}