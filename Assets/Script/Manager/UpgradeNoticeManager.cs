using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
public class UpgradeNoticeManager : Singleton<UpgradeNoticeManager>
{
    [SerializeField] Animator myanim;
    [SerializeField] TextMeshProUGUI characterLvl_txt;
    [SerializeField] TextBar textBarprefab;
    [SerializeField] Transform textBarContent;
    [SerializeField] Button confirm_btn;
    public int MAX_ATTRIBUTE;
    public List<TextBar> textBarList;

    private void OnDisable()
    {
        confirm_btn.onClick.RemoveListener(DisableUpgradeNotice);
        textBarList.ForEach(x => x.gameObject.SetActive(false));
    }
    public void SetLevelText(string _level) => characterLvl_txt.text = $"Lv.{_level}";
    public void CreateNoticeBar(int _index, string _titleAttribute, string _value1, string _value2)
    {
        textBarList[_index].SetTitleText(_titleAttribute);
        textBarList[_index].SetValueBefore(_value1);
        textBarList[_index].SetValueAfter(_value2);
    }
    public void SpawnNoticeUpgrade()
    {
        for (int i = 0; i < MAX_ATTRIBUTE; i++)
        {
            var _textbar = PoolManager.instance.Release(textBarprefab.gameObject);
            _textbar.transform.SetParent(textBarContent);
            _textbar.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        textBarList = textBarContent.GetComponentsInChildren<TextBar>().ToList();
        confirm_btn.onClick.AddListener(DisableUpgradeNotice);
        myanim.Play("EnableUpgradeNotice");
        AudioManager.instance.PlaySfx("Click");
    }
    private void DisableUpgradeNotice()
    {
        myanim.Play("DisableUpgradeNotice");
        AudioManager.instance.PlaySfx("Click");
        textBarList.ForEach(x => x.gameObject.SetActive(false));
    }
}
