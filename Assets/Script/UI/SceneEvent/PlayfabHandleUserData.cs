using UnityEngine.Events;
using PlayFab.ClientModels;
using PlayFab;

public class PlayfabHandleUserData : Singleton<PlayfabHandleUserData>
{
    public UnityEvent OnLoadDataUserLoginSucces;
    public UnityEvent OnLoadDataUserLoginFailed;

    private bool isLogin = false;
    public enum PlayfabKey
    {
        UserData_Key,
        PlayerData_Key,
        ShopData_Key,
    }
    private void Start()
    {
        isLogin = false;
        GetInternalGameData();
        PlayfabController.instance.OnLoginSuccesEvent += OnLoginSucces;
    }
    private void OnDestroy()
    {
        PlayfabController.instance.OnLoginSuccesEvent -= OnLoginSucces;
    }
    private void OnApplicationQuit()
    {
        if (isLogin)
            UpdateAllData();
    }
    private void OnLoginSucces()
    {
        isLogin = true;
        GetUserData();
    }
    private void GetUserData()
    {
        if (!isLogin)
            return;
        OnLoadDataUserLoginSucces?.Invoke();
        var request = new GetUserDataRequest { };
        PlayFabClientAPI.GetUserData(request, OnGetDataResult, ErrorCallBack);
    }
    private void OnGetDataResult(GetUserDataResult result)
    {


    }
    private void ErrorCallBack(PlayFabError error)
    {

    }
    private void UpdateAllData()
    {

    }
    private void GetInternalGameData()
    {

    }
}