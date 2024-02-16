using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;
public class LoginPlayfab : Singleton<LoginPlayfab>
{
    [SerializeField] TextMeshProUGUI loginTitle_txt;
    [SerializeField] GameObject startMenuPanel;
    [SerializeField] TextMeshProUGUI message_txt;
    [Header("       Login")]
    [SerializeField] GameObject loginprefab;
    [SerializeField] TMP_InputField loginEmailInput;
    [SerializeField] TMP_InputField loginPasswordInput;

    [Header("       Register")]
    [SerializeField] GameObject registerprefab;
    [SerializeField] TMP_InputField registerUserInput;
    [SerializeField] TMP_InputField registerEmailInput;
    [SerializeField] TMP_InputField registerPasswordInput;

    [Header("       Recover")]
    [SerializeField] GameObject recoverprefab;
    [SerializeField] TMP_InputField recoverUserInput;

    [Header("       Info Account")]
    public readonly string playfabID = "EFB1C";
    public readonly string Key_Name = "";
    public readonly string Key_Gmail = "";
    public readonly string Key_PW = "";

    // event login
    public event Action OnLoginSuccesEvent;
    public event Action OnRegisterSuccesEvent;

    protected override void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {
        startMenuPanel.gameObject.SetActive(true);
        loginTitle_txt.text = string.Empty;
        message_txt.text = string.Empty;
    }
    public void OnLoginPage()
    {
        loginTitle_txt.text = "Login";
        loginprefab.gameObject.SetActive(true);
        registerprefab.gameObject.SetActive(false);
        recoverprefab.gameObject.SetActive(false);
        startMenuPanel.gameObject.SetActive(false);
        loginEmailInput.text = "";
        loginPasswordInput.text = "";
    }
    public void OnRegisterPage()
    {
        loginTitle_txt.text = "Register";
        registerprefab.gameObject.SetActive(true);
        loginprefab.gameObject.SetActive(false);
        recoverprefab.gameObject.SetActive(false);
        startMenuPanel.gameObject.SetActive(false);
        registerEmailInput.text = "";
        registerPasswordInput.text = "";
        registerUserInput.text = "";
    }
    public void OnRecoverPage()
    {
        loginTitle_txt.text = "Recover";
        recoverprefab.gameObject.SetActive(true);
        registerprefab.gameObject.SetActive(false);
        loginprefab.gameObject.SetActive(false);
        startMenuPanel.gameObject.SetActive(false);
        recoverUserInput.text = "";
    }
    public void RegisterUser()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = registerUserInput.text,
            Email = registerEmailInput.text,
            Password = registerPasswordInput.text,
            TitleId = playfabID,
            RequireBothUsernameAndEmail = false,
        };
        PlayFabClientAPI.RegisterPlayFabUser(request,
            registerSucces =>
            {
                message_txt.text = "New Acount is Created";
                PlayerPrefs.SetString(Key_Name, registerUserInput.ToString());
                OnLoginPage();
                OnRegisterSuccesEvent?.Invoke();
            },
            OnError);
    }
    public void LoginUser()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = loginEmailInput.text,
            Password = loginPasswordInput.text,
            TitleId = playfabID,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request,
            resuilt =>
            {
                SaveAccount();
                OnLoginSucces(resuilt);
                OnLoginSuccesEvent?.Invoke();
                gameObject.SetActive(false);
            },
            OnError);
    }
    private void SaveAccount()
    {
        PlayerPrefs.SetString(Key_Name, registerUserInput.text.ToString());
        PlayerPrefs.SetString(Key_Gmail, loginEmailInput.text.ToString());
        PlayerPrefs.SetString(Key_PW, loginPasswordInput.text.ToString());
    }
    private void OnLoginSucces(LoginResult result)
    {
        message_txt.text = "Login Succes";
        loginprefab.gameObject.SetActive(false);
        SceneManager.LoadScene("Base 1");
    }
    public void RecoverUser()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = recoverUserInput.text,
            TitleId = playfabID,
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverSucces, OnRecoveryError);
    }
    private void OnRecoverSucces(SendAccountRecoveryEmailResult result)
    {
        OnLoginPage();
        message_txt.text = "Recovery Mail Sent";
    }
    private void OnRecoveryError(PlayFabError error)
    {
        message_txt.text = "No Email Found";
    }
    public void LoadAccount()
    {
        if (!PlayerPrefs.HasKey(Key_Gmail)) // not same account
            return;

    }
    private void OnError(PlayFabError error)
    {
        message_txt.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
    public void OnClick()
    {
        startMenuPanel.gameObject.SetActive(true);
        loginTitle_txt.text = string.Empty;
        message_txt.text = string.Empty;
    }
}
