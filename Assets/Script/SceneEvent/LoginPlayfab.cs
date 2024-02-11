using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;
public class LoginPlayfab : MonoBehaviour
{
    int a = 1;
    [SerializeField] TextMeshProUGUI loginTitle_txt;
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
        loginTitle_txt.gameObject.SetActive(false);
    }
    public void OnLoginPage()
    {
        loginTitle_txt.text = "Login";
        loginprefab.gameObject.SetActive(true);
        registerprefab.gameObject.SetActive(false);
        loginTitle_txt.gameObject.SetActive(true);
    }
    public void OnRegisterPage()
    {
        loginTitle_txt.text = "Register";
        registerprefab.gameObject.SetActive(true);
        loginprefab.gameObject.SetActive(false);
        loginTitle_txt.gameObject.SetActive(true);
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
        //message_txt.text = "New Acount is Created";
        OnLoginPage();
    }
    private void OnError(PlayFabError error)
    {
        //message_txt.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
}
