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
    [SerializeField] GameObject textBarprefab;
    [SerializeField] Transform textBarContent;
    List<GameObject> textBarList;
    [SerializeField] Button confirm_btn;
    const int MAX = 5;
    public int MAX_ATTRIBUTE => MAX;  // hp , mp , atk , def , dmgx
    private void Start()
    {
        for (int i = 0; i < MAX_ATTRIBUTE; i++)
        {
            var textBar = PoolManager.instance.Release(textBarprefab);
            textBar.transform.SetParent(textBarContent);
        }
        textBarList = textBarContent.GetComponentsInChildren<GameObject>().ToList();
        confirm_btn.onClick.AddListener(DisableUpgradeNotice);
    }
    private void OnDestroy()
    {
        confirm_btn.onClick.RemoveListener(DisableUpgradeNotice);
    }
    public void SetLevelText(string _level) => characterLvl_txt.text = _level;
    public static void CreateNoticeBar(string _titleAttribute, string _value1, string _value2)
    {
        foreach (var statsText in textBarList)
        {
            var text = statsText.GetComponentInParent<TextMeshPro>();
            text.text = $"{_titleAttribute}         {_value1}               {_value2}";
        }
    }
    public void EnableUpgradeNotice() => myanim.Play("EnableUpgradeNotice");
    private void DisableUpgradeNotice()
    {
        myanim.Play("DisableUpgradeNotice");
        textBarList.ForEach(x => x.gameObject.SetActive(false));
    }
}
