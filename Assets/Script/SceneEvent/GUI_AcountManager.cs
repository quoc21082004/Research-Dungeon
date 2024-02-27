using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using UnityEditor;
using DG.Tweening;
public class GUI_AcountManager : MonoBehaviour
{
    [SerializeField] GameObject panelAnimateLoad;
    [SerializeField] Button logout_btn;
    [SerializeField] Button account_btn;

    [SerializeField] Button startgame_btn;
    [SerializeField] GUI_Login guiLogin;
    [SerializeField] GUI_Register guiRegister;
    [SerializeField] GUI_ForgetPW guiForgetPW;

    [SerializeField] private RectTransform noticeframe;
    [SerializeField] private TextMeshProUGUI notice_txt;

    [SerializeField] private TextMeshProUGUI accountID_txt;
    [SerializeField] private TextMeshProUGUI email_txt;

    private Coroutine handleCoroutine;
    private Coroutine noticeCoroutine;
    private void Start()
    {
        Init();
        RegisterEvent();
    }
    private void OnEnable()
    {
        RegisterEvent();
    }
    private void OnDisable()
    {
        UnRegisterEvent();
    }
    private void Init()
    {
        FadeManager.instance.OnFadeOutComplete();
        panelAnimateLoad.gameObject.SetActive(false);
        account_btn.gameObject.SetActive(true);
        startgame_btn.gameObject.SetActive(false);
        logout_btn.gameObject.SetActive(true);
        accountID_txt.text = "";
        email_txt.text = "";
    }
    private void RegisterEvent()
    {
        logout_btn.onClick.AddListener(LogOutAccount);
        account_btn.onClick.AddListener(OpenPanelLogin);

        if (PlayfabController.instance)             // have value = load data
        {
            #region load Data
            PlayfabHandleUserData.instance.OnLoadDataUserLoginSucces.AddListener(OnLoadDataUserSucces);
            PlayfabHandleUserData.instance.OnLoadDataUserLoginFailed.AddListener(OnLoadDataUserFailed);
            #endregion
        }

        if (!PlayfabController.instance)
            return;
        #region     Login
        guiLogin.start_btn.onClick.AddListener(PlayfabController.instance.OnLogin);
        guiLogin.loginEmailField.onValueChanged.AddListener(PlayfabController.instance.SetUserEmail);
        guiLogin.loginPassWordField.onValueChanged.AddListener(PlayfabController.instance.SetUserPW);
        PlayfabController.instance.OnLoginSuccesEvent += HandleLoginSucces;
        PlayfabController.instance.OnLoginFailedEvent += HandleLoginFailed;
        PlayfabController.instance.OnLoginFailedEvent += guiLogin.SetErrorText;
        #endregion

        #region Register
        guiRegister.register_btn.onClick.AddListener(PlayfabController.instance.OnRegister);
        guiRegister.usernameField.onValueChanged.AddListener(PlayfabController.instance.SetUserName);
        guiRegister.emailField.onValueChanged.AddListener(PlayfabController.instance.SetUserEmail);
        guiRegister.passwordField.onValueChanged.AddListener(PlayfabController.instance.SetUserPW);
        PlayfabController.instance.OnRegisterSuccesEvent += HandleRegisterSucces;
        PlayfabController.instance.OnRegisterFailedEvent += guiRegister.SetErrorText;
        #endregion

        #region ForgetPW
        guiForgetPW.veficatate_btn.onClick.AddListener(PlayfabController.instance.OnForgetPW);
        guiForgetPW.emailField.onValueChanged.AddListener(PlayfabController.instance.SetUserEmail);
        PlayfabController.instance.OnMailSendSuccesEvent += HandleForgetPwSucces;
        PlayfabController.instance.OnMailSendFailedEvent += guiForgetPW.SetErrorText;
        #endregion

        PlayfabController.instance.OnLogin();
    }
    private void UnRegisterEvent()
    {
        logout_btn.onClick.RemoveListener(LogOutAccount);
        account_btn.onClick.RemoveListener(OpenPanelLogin);

        if (PlayfabController.instance)
        {
            #region loadData
            PlayfabHandleUserData.instance.OnLoadDataUserLoginSucces.RemoveListener(OnLoadDataUserSucces);
            PlayfabHandleUserData.instance.OnLoadDataUserLoginFailed.RemoveListener(OnLoadDataUserFailed);
            #endregion
        }
        #region     Login
        guiLogin.start_btn.onClick.RemoveListener(PlayfabController.instance.OnLogin);
        guiLogin.loginEmailField.onValueChanged.RemoveListener(PlayfabController.instance.SetUserEmail);
        guiLogin.loginPassWordField.onValueChanged.RemoveListener(PlayfabController.instance.SetUserPW);
        PlayfabController.instance.OnLoginSuccesEvent -= HandleLoginSucces;
        PlayfabController.instance.OnLoginFailedEvent -= HandleLoginFailed;
        PlayfabController.instance.OnLoginFailedEvent -= guiLogin.SetErrorText;
        #endregion

        #region Register
        guiRegister.register_btn.onClick.RemoveListener(PlayfabController.instance.OnRegister);
        guiRegister.usernameField.onValueChanged.RemoveListener(PlayfabController.instance.SetUserName);
        guiRegister.emailField.onValueChanged.RemoveListener(PlayfabController.instance.SetUserEmail);
        guiRegister.passwordField.onValueChanged.RemoveListener(PlayfabController.instance.SetUserPW);
        PlayfabController.instance.OnRegisterSuccesEvent -= HandleRegisterSucces;
        PlayfabController.instance.OnRegisterFailedEvent -= guiRegister.SetErrorText;
        #endregion

        #region ForgetPW
        guiForgetPW.veficatate_btn.onClick.RemoveListener(PlayfabController.instance.OnForgetPW);
        guiForgetPW.emailField.onValueChanged.RemoveListener(PlayfabController.instance.SetUserEmail);
        PlayfabController.instance.OnMailSendSuccesEvent -= HandleForgetPwSucces;
        PlayfabController.instance.OnMailSendFailedEvent -= guiForgetPW.SetErrorText;
        #endregion
    }
    public void OpenPanelLogin()
    {

    }
    public void LogOutAccount()
    {
        ClearAccountTemp();
        email_txt.text = "";
        accountID_txt.text = "";
        account_btn.gameObject.SetActive(true);
        logout_btn.gameObject.SetActive(false);
        startgame_btn.gameObject.SetActive(false);
    }
    public void HandleLoginSucces()
    {
        if (handleCoroutine != null)
            StopCoroutine(handleCoroutine);
        handleCoroutine = StartCoroutine(LoginSuccesCoroutine());
    }
    private IEnumerator LoginSuccesCoroutine()
    {
        panelAnimateLoad.gameObject.SetActive(true);
        yield return new WaitForSeconds(Random.Range(1, 1.4f));
        guiLogin.gameObject.SetActive(false);
        panelAnimateLoad.gameObject.SetActive(false);
        logout_btn.gameObject.SetActive(true);
        account_btn.gameObject.SetActive(false);
        startgame_btn.gameObject.SetActive(true);
        GetUserInfo();
        if (noticeCoroutine != null)
            StopCoroutine(noticeCoroutine);
        noticeCoroutine = StartCoroutine(ShowNoticeText("Login Succesful"));
    }
    private void HandleLoginFailed(string _loginfalse)
    {
        panelAnimateLoad.gameObject.SetActive(false);
        OpenPanelLogin();
    }
    private void HandleRegisterSucces()
    {
        if (handleCoroutine != null)
            StopCoroutine(handleCoroutine);
        handleCoroutine = StartCoroutine(RegisterSuccesCoroutine());
    }
    private IEnumerator RegisterSuccesCoroutine()
    {
        panelAnimateLoad.gameObject.SetActive(true);
        yield return new WaitForSeconds(Random.Range(0.85f, 1.5f));
        guiRegister.gameObject.SetActive(false);
        guiLogin.gameObject.SetActive(true);
        panelAnimateLoad.gameObject.SetActive(false);
        if (noticeCoroutine != null)
            StopCoroutine(noticeCoroutine);
        noticeCoroutine = StartCoroutine(ShowNoticeText("Register Succesful"));
    }
    private IEnumerator ShowNoticeText(string _noticetxt)
    {
        noticeframe.gameObject.SetActive(true);
        noticeframe.anchoredPosition = new Vector2(0, 445f);
        notice_txt.gameObject.SetActive(true);
        notice_txt.text = _noticetxt.ToString();
        Vector2 endY = new Vector2(0, 312f);
        noticeframe.DOAnchorPos(endY, 1f);
        yield return new WaitForSeconds(2.25f);
        notice_txt.gameObject.SetActive(false);
        noticeframe.gameObject.SetActive(false);
    }
    private void HandleForgetPwSucces()
    {
        if (handleCoroutine != null)
            StopCoroutine(handleCoroutine);
        handleCoroutine = StartCoroutine(ForgetPwSuccesCoroutine());
    }
    private IEnumerator ForgetPwSuccesCoroutine()
    {
        panelAnimateLoad.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        panelAnimateLoad.gameObject.SetActive(false);

        if (noticeCoroutine != null)
            StopCoroutine(noticeCoroutine);
        noticeCoroutine = StartCoroutine(ShowNoticeText("Password change request succesful"));
    }
    private void OnLoadDataUserSucces() { }
    private void OnLoadDataUserFailed() => Debug.Log("failed data");
    private void GetUserInfo()
    {
        if (!PlayfabController.instance)
            return;
        accountID_txt.text = "ID:" + PlayfabController.instance.userID;
        var email = PlayfabController.instance.userEmail;       // vunguyenquoc@gmail.com
        var mailTemp = $"{email[0]}{email[1]}";
        var last_id = 0;
        for (int i = 2; i < email.Length; i++)           // hide => vu********@gmail.com
        {
            if (email[i] == '@')
            {
                last_id = i;
                break;
            }
            mailTemp += '*';
        }
        mailTemp += email.Substring(last_id);               // remain text "gmail.com"
        email_txt.text =  "Welcome "+ $"<color=#10C7FF>User:</color> <color=#FFCD10>{mailTemp}</color>"; ;
    }
    public void ClearAccountTemp()
    {
        if (PlayfabController.instance)
            PlayfabController.instance.ClearAccountTemp();
    }
}