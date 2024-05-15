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

    [SerializeField] private TextMeshProUGUI completeQuest_txt;
    [SerializeField] private Animator completeQuestNotice;
    [SerializeField] private Animator completeChallengeNotice;

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
    public void EnableCompleteQuest() => completeQuestNotice.Play("CompleteQuest_In");
    public void DisableCompleteQuest() => completeQuestNotice.Play("CompleteQuest_Out");
    public void EnableCompleteChallenge() => completeChallengeNotice.Play("CompleteChallenge_In");
    public void DisableCompleteChallenge() => completeChallengeNotice.Play("CompleteChallenge_Out");
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