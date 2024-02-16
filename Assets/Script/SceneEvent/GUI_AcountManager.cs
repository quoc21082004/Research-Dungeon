using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GUI_AcountManager : MonoBehaviour
{
    [SerializeField] Button logoutbtn;
    [SerializeField] Button accountbtn;

    [SerializeField] private Animator animator;

    [SerializeField] Button startgamebtn;
    [SerializeField] GUI_Login guiLogin;
    [SerializeField] GUI_Register guiRegister;
    [SerializeField] GUI_ForgetPW guiForgetPW;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
}