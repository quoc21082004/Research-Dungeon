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
    [SerializeField] TMP_InputField loginUserInput;
    [SerializeField] TMP_InputField loginPasswordInput;

    [Header("       Register")]
    [SerializeField] GameObject registerprefab;
    [SerializeField] TMP_InputField registerUserInput;
    [SerializeField] TMP_InputField registerEmailInput;
    [SerializeField] TMP_InputField registerPasswordInput;

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
        startMenuPanel.gameObject.SetActive(false);
    }
    public void OnRegisterPage()
    {
        loginTitle_txt.text = "Register";
        registerprefab.gameObject.SetActive(true);
        loginprefab.gameObject.SetActive(false);
        startMenuPanel.gameObject.SetActive(false);
    }
    public void OnRecoverPage()
    {

    }
    public void RegisterUser()
    {
        var request = new RegisterPlayFabUserRequest
        {
            DisplayName = registerUserInput.text,
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
        var request = new LoginWithPlayFabRequest
        {
            Username = loginUserInput.text,
            Password = loginPasswordInput.text,
        };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSucces, OnError);
    }
    private void OnLoginSucces(LoginResult result)
    {
        message_txt.text = string.Empty;
        loginprefab.gameObject.SetActive(false);
        SceneManager.LoadScene("Base 1");
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
