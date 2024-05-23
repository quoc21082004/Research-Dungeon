using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class GUI_Login : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI error_txt;
    [SerializeField] public TMP_InputField loginEmailField;
    [SerializeField] public TMP_InputField loginPassWordField;
    [SerializeField] public Button start_btn;
    [SerializeField] public Button quit_btn;

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
        start_btn.onClick?.Invoke();
        AudioManager.instance.PlaySfx("Click");
    }
    private void OnESCInput(InputAction.CallbackContext context)
    {
        quit_btn.onClick?.Invoke();
        AudioManager.instance.PlaySfx("Click");
    }
    public void SetErrorText(string _errorText)
    {
        error_txt.text = _errorText;
        Invoke(nameof(SetDefaultErrorText), 6f);
    }
    private void SetDefaultErrorText() => error_txt.text = "";
    private void SetDefaultLoginText()
    {
        loginEmailField.text = "";
        loginPassWordField.text = "";
        loginEmailField.Select();
    }

}