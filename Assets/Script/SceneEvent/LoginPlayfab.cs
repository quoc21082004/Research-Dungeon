using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PlayFab;
public class LoginPlayfab : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI loginTitle_txt;
    [Header("       Login")]
    [SerializeField] GameObject loginprefab;
    [SerializeField] TMP_InputField loginInput;
    [SerializeField] TMP_InputField loginPasswordInput;

    [Header("       Register")]
    [SerializeField] GameObject registerprefab;
    [SerializeField] TMP_InputField registerInput;
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
        var request = new Reg
    }
}
