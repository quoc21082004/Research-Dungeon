using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayfabController : Singleton<PlayfabController>
{
    public event Action OnLoginSuccesEvent;
    public event Action<string> OnLoginFailedEvent;
    public event Action OnRegisterSuccesEvent;
    public event Action<string> OnRegisterFailedEvent;
    public event Action OnMailSendSuccesEvent;
    public event Action<string> OnMailSendFailedEvent;

    private readonly string TitleID = "59B82";
    private readonly string USERNAME_Key = "USERNAME";
    private readonly string EMAIL_Key = "EMAIL";
    private readonly string PW_Key = "PASSWORD";
    public string userID;
    public string username;
    public string userEmail;
    public string userPassword;
    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = TitleID;
        }
    }
    public void OnLogin()
    {
        LoadAccount();
        var request = new LoginWithEmailAddressRequest
        {
            Email = userEmail,
            Password = userPassword,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request,
            result =>
            {
                SaveAccount();
                GetAccountID();
                GetUsername();
                OnLoginSuccesEvent?.Invoke();
            },
            error =>
            {
                OnLoginFailedEvent?.Invoke(OnLoginErrorCode(error));
            }
        );
    }
    public void OnRegister()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = username,
            Email = userEmail,
            Password = userPassword,
        };
        PlayFabClientAPI.RegisterPlayFabUser(request,
            result =>
            {
                PlayerPrefs.SetString(USERNAME_Key, username);
                OnRegisterSuccesEvent?.Invoke();
            },
            error =>
            {
                OnRegisterFailedEvent?.Invoke(OnRegisterErrorCode(error));
            }
        );
    }
    public void OnForgetPW()
    {
        var request = new SendAccountRecoveryEmailRequest
        { 
            Email = userEmail,
            TitleId = TitleID,
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request,
            result =>
            {
                OnMailSendSuccesEvent?.Invoke();
            },
            error =>
            {
                OnMailSendFailedEvent?.Invoke(OnForgotPWErrorCode(error));
            }
        );
    }

    private static string OnLoginErrorCode(PlayFabError _error)
    {
        var keyMessage = _error.Error switch
        {
            PlayFabErrorCode.InvalidUsernameOrPassword
            or PlayFabErrorCode.InvalidEmailOrPassword
            or PlayFabErrorCode.InvalidUsername
            or PlayFabErrorCode.InvalidPassword
            or PlayFabErrorCode.AccountNotFound
            or PlayFabErrorCode.InvalidParams
                => "Invalid Email Or Password",
            _ => $"{_error.Error}"
        };
        return keyMessage;
    }
    private static string OnRegisterErrorCode(PlayFabError _error)
    {
        var keyMessage = _error.Error switch
        {
            PlayFabErrorCode.InvalidParams or PlayFabErrorCode.AccountNotFound => "Invalid Account !",
            PlayFabErrorCode.UsernameNotAvailable or PlayFabErrorCode.InvalidUsername => "Username Not Available !",
            PlayFabErrorCode.EmailAddressNotAvailable or PlayFabErrorCode.InvalidEmailAddress => "Email Address Not Available !",
            _ => $"{_error.Error}"
        };
        return keyMessage;
    }
    private static string OnForgotPWErrorCode(PlayFabError _error)
    {
        var keyMessage = _error.Error switch
        {
            PlayFabErrorCode.InvalidUsernameOrPassword
            or PlayFabErrorCode.InvalidEmailAddress
            or PlayFabErrorCode.EmailAddressNotAvailable
            or PlayFabErrorCode.InvalidEmailOrPassword
            or PlayFabErrorCode.AccountNotFound
            or PlayFabErrorCode.InvalidParams => "Invalid Email Address",
            _ => $"{_error.Error}"
        };
        return keyMessage;
    }
    private void SaveAccount()
    {
        PlayerPrefs.SetString(USERNAME_Key, username);
        PlayerPrefs.SetString(EMAIL_Key, userEmail);
        PlayerPrefs.SetString(PW_Key, userPassword);
    }
    private void LoadAccount()
    {
        if (!PlayerPrefs.HasKey(EMAIL_Key))
        {
            OnLoginFailedEvent?.Invoke("");
            return;
        }
        username = PlayerPrefs.GetString(USERNAME_Key);
        userEmail = PlayerPrefs.GetString(EMAIL_Key);
        userPassword = PlayerPrefs.GetString(PW_Key);
    }
    public void ClearAccountTemp()
    {
        userID = "";
        username = "";
        userEmail = "";
        userPassword = "";
        PlayerPrefs.DeleteKey(USERNAME_Key);
        PlayerPrefs.DeleteKey(EMAIL_Key);
        PlayerPrefs.DeleteKey(PW_Key);
    }
    private void GetAccountID()
    {
        var _request = new GetPlayerProfileRequest()
        {
            PlayFabId = PlayFabSettings.staticPlayer.PlayFabId
        };
        PlayFabClientAPI.GetPlayerProfile(_request, result =>
        {
            if (result.PlayerProfile is { PlayerId: not null })
            {
                userID = result.PlayerProfile.PlayerId;
            }
        }, _ => { });
    }
    private void GetUsername()
    {
        var _request = new GetAccountInfoRequest()
        {
            PlayFabId = PlayFabSettings.staticPlayer.PlayFabId
        };
        PlayFabClientAPI.GetAccountInfo(_request, result =>
        {
            if (result.AccountInfo is { TitleInfo: not null })
            {
                username = result.AccountInfo.Username;
            }
        }, _ => { });
    }
    public void SetUserName(string _nameIn) => username = _nameIn;
    public void SetUserEmail(string _emailIn) => userEmail = _emailIn;
    public void SetUserPW(string _passwordIn) => userPassword = _passwordIn;
}
