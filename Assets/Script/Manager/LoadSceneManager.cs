using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    public GameObject loadingScene;
    private Coroutine loadCoroutine;
    public Image loadingbarFill;

    public void LoadScene(int sceneID)
    {
        if (loadCoroutine != null)
            StopCoroutine(loadCoroutine);
        loadCoroutine = StartCoroutine(_loadCoroutine(sceneID));
    }
    private IEnumerator _loadCoroutine(int sceneID)
    {
        var operation = SceneManager.LoadSceneAsync(sceneID);
        loadingScene.gameObject.SetActive(true);
        while (!operation.isDone)
        {
            float progessValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingbarFill.fillAmount = progessValue;
            yield return null;
        }
        loadingScene.gameObject.SetActive(false);
    }
}