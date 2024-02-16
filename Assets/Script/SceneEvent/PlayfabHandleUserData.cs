using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
public class PlayfabHandleUserData : Singleton<PlayfabHandleUserData>
{
    public UnityEvent OnLoadDataUserLoginSucces;
    public UnityEvent OnLoadDataUserLoginFailed;

}