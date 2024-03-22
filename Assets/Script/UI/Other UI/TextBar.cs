﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleAttribute_txt;
    [SerializeField] TextMeshProUGUI valuebefore_txt;
    [SerializeField] TextMeshProUGUI valueafter_txt;
    private void Start() => transform.localScale = new Vector3(1f, 1f, 1f);
    public void SetTitleText(string _value) => titleAttribute_txt.text = _value;
    public void SetValueBefore(string _value) => valuebefore_txt.text = _value;
    public void SetValueAfter(string _value) => valueafter_txt.text = _value;


}