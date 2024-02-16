using System.Collections;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
public class PlayfabController : Singleton<PlayfabController>
{
    //                          info
    public event Action OnLoginSuccesEvent;
    public event Action<string> OnLoginFailedEvent;
    public event Action OnRegisterSuccesEvent;
    public event Action<string> OnRegisterFailedEvent;
    public event Action OnMailSendSuccesEvent;
    public event Action<string> OnMailSendFailedEvent;

    private readonly string titleID = "EFB1C";
    private readonly string userName_key = "USER";
    private readonly string email_key = "Email";
    private readonly string password_key = "password";

    public string userID;
    public string userName;
    public string userEmail;
    public string userPW;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
       
    }
                        #region On Login
    public void OnLogin()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = userEmail,
            Password = userPW,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request,
            result =>
            {
                LoadAccount();
                GetAccountID();
                GetUserName();
                OnLoginSuccesEvent?.Invoke();
            },
            error =>
            {
                OnLoginFailedEvent?.Invoke(OnLoginError(error));
            });
    }

    public string OnLoginError(PlayFabError errorCode)
    {
        return "";
    }
    #endregion


                        #region On Register
    private void OnRegister()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = userName,
            Email = userEmail,
            Password = userPW,
        };
        PlayFabClientAPI.RegisterPlayFabUser(request,
            resuilt =>
            {
                PlayerPrefs.SetString(userName_key, userName);
                OnRegisterSuccesEvent?.Invoke();
            },
            error =>
            {
                OnRegisterFailedEvent?.Invoke(OnRegisterError(error));
            });
    }
    private string OnRegisterError(PlayFabError error)
    {
        throw new NotImplementedException();
    }
    #endregion

                #region On ForgetPW
    private void OnForgetPW()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = userEmail,
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request,
            result =>
            {
                OnMailSendSuccesEvent?.Invoke();
            },
            error =>
            {
                OnMailSendFailedEvent?.Invoke(OnForgetError(error));
            });
    }
    private string OnForgetError(PlayFabError error)
    {
        throw new NotImplementedException();
    }
    #endregion
    // resuilt callback playfab
    private void LoadAccount()
    {
        if (!PlayerPrefs.HasKey(email_key))     // not same gmail when register - false;
        {
            OnLoginFailedEvent?.Invoke("");
            return;
        }
        userName = PlayerPrefs.GetString(userName_key);
        userEmail = PlayerPrefs.GetString(email_key);
        userPW = PlayerPrefs.GetString(userPW);
    }
    private void GetAccountID()
    {

    }
    private void GetUserName()
    {

    }
}