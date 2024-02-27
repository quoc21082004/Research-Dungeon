using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : Singleton<FadeManager>
{
    private Animator myanim;
    public string levelToLoad;
    public static bool fadeDone;

    private void OnEnable()
    {
        myanim = GetComponent<Animator>();
    }
    public void OnFade()
    {
        fadeDone = false;
    }
    public void OnFadeInComplete()
    {
        myanim.ResetTrigger("Fade_In");
        if (levelToLoad == "")
        {
            fadeDone = true;
            return;
        }
        levelToLoad = "";
        myanim.SetTrigger("Fade_Out");
    }
    public void OnFadeOutComplete()
    {
        myanim.ResetTrigger("Fade_Out");
        fadeDone = true;
    }
    public void OnFadeSceneChange()
    {
        fadeDone = false;
        myanim.SetTrigger("Fade_In");
    }
}
