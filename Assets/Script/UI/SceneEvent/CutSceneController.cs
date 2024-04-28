using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections;
using System;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
public class CutSceneController : MonoBehaviour
{
    [SerializeField] Animator fadingAnim;
    [SerializeField] Transform objectMove;
    [SerializeField] DialogueObject[] dialogueStore;
    [SerializeField] Image fade_img;
    [SerializeField] CinemachineVirtualCamera virtualCineCamera;
    [SerializeField] UnityEvent[] cutSceneEvent;

    private int currentAction = 0;
    private float moveSpeed;
    private int runningAction = 0;
    private float cameraChangeSizeSpeed = 12f;

    private Coroutine moveCoroutine;
    private Coroutine cameraSizeCoroutine;
    private Coroutine delayActionCoroutine;


    private IEnumerator Start()
    {
        while (currentAction < cutSceneEvent.Length)
        {
            runningAction = 0;
            cutSceneEvent[currentAction++]?.Invoke();
            yield return new WaitWhile(() => runningAction > 0);
        }
        SkipCutScene();
    }

    #region Move Camera
    public void MoveTo(Transform _target)
    {
        runningAction++;
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            runningAction--;
        }
        moveCoroutine = StartCoroutine(MoveToCoroutine(_target));
    }
    private IEnumerator MoveToCoroutine(Transform _target)
    {
        float distance = Vector2.Distance(objectMove.transform.position, _target.position);
        while (distance > 0.15f)
        {
            objectMove.transform.position = Vector2.Lerp(objectMove.transform.position, _target.position, moveSpeed * Time.fixedDeltaTime);
            distance = Vector2.Distance(objectMove.transform.position, _target.position); // update distance for loop
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        moveCoroutine = null;
        runningAction--;
    }
    public void SetMoveSpeed(float _setSpeed) => moveSpeed = _setSpeed;
    #endregion

    #region Show Dialogue 
    public void ShowDialogue(int index)
    {
        if (DialogueManager.instance.IsUsing)
            runningAction--;
        runningAction++;
        if (index > dialogueStore.Length || index < 0)
            Debug.Log("Don't have enough storage dialogue " + index);
        StartCoroutine(ShowDialogueCoroutine(Mathf.Clamp(index, 0, dialogueStore.Length - 1))); //
    }
    private IEnumerator ShowDialogueCoroutine(int index)
    {
        DialogueManager.instance.ShowDialogue(dialogueStore[index]);
        yield return new WaitWhile(() => DialogueManager.instance.IsUsing);
        runningAction--;
    }
    #endregion

    #region Change Camera Size
    public void ChangeCameraSize(float _size)
    {
        runningAction++;
        if (cameraSizeCoroutine != null)
        {
            StopCoroutine(cameraSizeCoroutine);
            runningAction--;
        }
        cameraSizeCoroutine = StartCoroutine(ChangeCameraSizeCoroutine(_size));
    }
    private IEnumerator ChangeCameraSizeCoroutine(float _size)
    {
        while (Mathf.Abs(virtualCineCamera.m_Lens.OrthographicSize - _size) > Mathf.Epsilon)
        {
            virtualCineCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(virtualCineCamera.m_Lens.OrthographicSize, _size, cameraChangeSizeSpeed * Time.fixedDeltaTime);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        cameraSizeCoroutine = null;
        runningAction--;
    }
    public void SetCameraSizeChange(float _setCameraSizeChange) => cameraChangeSizeSpeed = _setCameraSizeChange;
    #endregion

    #region Delay Action End
    public void DelayActionEnd(int _delay)
    {
        runningAction++;
        if (delayActionCoroutine != null)
        {
            StopCoroutine(delayActionCoroutine);
            runningAction--;
        }
        delayActionCoroutine = StartCoroutine(DelayActionEndCoroutine(_delay));
    }
    private IEnumerator DelayActionEndCoroutine(int _delay)
    {
        yield return new WaitForSeconds(_delay);
        delayActionCoroutine = null;
        runningAction--;
    }
    #endregion

    public void SetFade(bool isState) => SetFade(isState, isState ? 1f : 0f);
    public void SetFade(bool isState, float duration)
    {
        if (isState)
            fadingAnim.Play("FadeIn");
        else
            fadingAnim.Play("FadeOut");
    }
    public void SkipCutScene()
    {
        StopAllCoroutines();
        StartCoroutine(SkipCutSceneCoroutine());
    }
    private IEnumerator SkipCutSceneCoroutine()
    {
        SetFade(true);
        yield return new WaitForSeconds(2f);
        DOTween.KillAll();
        LoadSceneManager.instance.LoadScene(1);
    }
}