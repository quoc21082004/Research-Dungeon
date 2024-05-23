using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GUI_Register : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI error_txt;
    [SerializeField] public TMP_InputField usernameField;
    [SerializeField] public TMP_InputField emailField;
    [SerializeField] public TMP_InputField passwordField;
    [SerializeField] public Button register_btn;
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
        register_btn.onClick?.Invoke();
        AudioManager.instance.PlaySfx("Click");
    }
    private void OnESCInput(InputAction.CallbackContext context)
    {
        quit_btn.onClick?.Invoke();
        AudioManager.instance.PlaySfx("Click");
    }
    public void SetErrorText(string _error)
    {
        error_txt.text = _error;
        Invoke(nameof(SetDefaultTextError), 6f);
    }
    public void SetDefaultTextError() => error_txt.text = "";
    public void SetDefaultFieldText()
    {
        usernameField.text = "";
        emailField.text = "";
        passwordField.text = "";
        usernameField.Select();
    }
}