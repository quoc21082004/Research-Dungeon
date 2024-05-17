using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class NoticeManager : Singleton<NoticeManager>
{
    #region Variable
    [SerializeField] private TextMeshProUGUI title_txt;
    [SerializeField] private TextBar2 textbar;
    [SerializeField] private Transform content;
    private Tween titleTween;
    float tweenTitleDuration = 0.5f;
    private Coroutine disableNoticeCoroutine;

    [Space]
    [SerializeField] private TextMeshProUGUI completeChallenge_txt;
    [SerializeField] private Animator completeChallengeNotice;
    private Coroutine disableChallengeCoroutine;
    [Space]
    [SerializeField] GameObject noticeInteractPanel;
    [SerializeField] TextMeshProUGUI noticeInteract_txt;
    #endregion

    private void Start() => title_txt.color = new Color(1f, 1f, 1f, 0f);
    
    #region Create Notice Left Item
    public void CreateNoticeLeftItem(Sprite _img, string _itemtext)
    {
        var _textbar = PoolManager.instance.Release(textbar.gameObject);
        _textbar.transform.SetParent(content);
        _textbar.GetComponent<TextBar2>().SetTextBar(_img, _itemtext);
    }
    public void EnableNoticeLeftItem()
    {
        titleTween?.Kill();
        titleTween = title_txt.DOColor(new Color(1f, 1f, 1f, 1f), tweenTitleDuration);
        if (disableNoticeCoroutine != null)
            StopCoroutine(DisableNoticeLeftItem());
        disableNoticeCoroutine = StartCoroutine(DisableNoticeLeftItem());
    }
    private IEnumerator DisableNoticeLeftItem()
    {
        yield return new WaitForSeconds(2.5f);
        titleTween?.Kill();
        titleTween = title_txt.DOColor(new Color(1f, 1f, 1f, 0f), tweenTitleDuration);
    }
    #endregion

    #region Create Notice Complete Quest or Complete Challenge
    public void EnableCompleteChallenge(string _completetext)
    {
        completeChallenge_txt.text = _completetext;
        completeChallengeNotice.Play("CompleteChallenge_In");
        if (disableChallengeCoroutine != null)
            StopCoroutine(disableChallengeCoroutine);
        disableChallengeCoroutine = StartCoroutine(DisableChallengeCoroutine());
    }
    private IEnumerator DisableChallengeCoroutine()
    {
        yield return new WaitForSeconds(2f);
        completeChallengeNotice.Play("CompleteChallenge_Out");
        completeChallenge_txt.text = string.Empty;
    }
    #endregion

    #region Create Notice Interact          
    public void CreateNoticeInteract(string _noticetext)
    {
        noticeInteractPanel.gameObject.SetActive(true);
        noticeInteract_txt.text = _noticetext.ToString();
    }
    public void CloseNoticeInteract() => noticeInteractPanel.gameObject.SetActive(false);
    #endregion
}