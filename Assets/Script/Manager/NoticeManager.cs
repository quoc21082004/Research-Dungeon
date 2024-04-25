using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NoticeManager : Singleton<NoticeManager>
{
    [SerializeField] GameObject noticeInteractPanel;
    [SerializeField] TextMeshProUGUI noticeInteract_txt;

    [SerializeField] Animator clearBossNotice;

    [SerializeField] GameObject regionPanel;
    [SerializeField] TextMeshProUGUI region_txt;
    [SerializeField] Animator regionNotice;

    #region Create Clear Boss

    
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