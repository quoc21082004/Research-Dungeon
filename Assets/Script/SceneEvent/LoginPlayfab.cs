using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;
public class LoginPlayfab : MonoBehaviour
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
    }
    public void OnRegisterPage()
    {
        loginTitle_txt.text = "Register";
        registerprefab.gameObject.SetActive(true);
        loginprefab.gameObject.SetActive(false);
        recoverprefab.gameObject.SetActive(false);
        startMenuPanel.gameObject.SetActive(false);
    }
    public void OnRecoverPage()
    {
        loginTitle_txt.text = "Recover";
        recoverprefab.gameObject.SetActive(true);
        registerprefab.gameObject.SetActive(false);
        loginprefab.gameObject.SetActive(false);
        startMenuPanel.gameObject.SetActive(false);
    }
    public void RegisterUser()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = registerUserInput.text,
            Email = registerEmailInput.text,
            Password = registerPasswordInput.text,
            RequireBothUsernameAndEmail = false,
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSucces, OnError);
    }
    private void OnRegisterSucces(RegisterPlayFabUserResult result)
    {
        message_txt.text = "New Acount is Created";
        OnLoginPage();
    }
    public void LoginUser()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = loginEmailInput.text,
            Password = loginPasswordInput.text,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSucces, OnError);
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
            TitleId = "EFB1C",
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
