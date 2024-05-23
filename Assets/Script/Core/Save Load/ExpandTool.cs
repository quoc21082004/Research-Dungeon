using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ExpandTool : MonoBehaviour
{
    [SerializeField] RectTransform expandToolTF;
    [Space]
    [SerializeField] Vector3 normalStateTF;
    [Space]
    [SerializeField] Vector3 expandStateTF;

    bool isDefaultExpand = true;
    private bool isNormal = true;
    private Tweener tween;
    void Start()
    {
        if (isDefaultExpand)
        {
            ToExpandState();
        }
    }
    public void ToNormalState()
    {
        isNormal = true;
        tween?.Kill();
        tween = expandToolTF.DOAnchorPos(normalStateTF, 1f);
    }
    public void ToExpandState()
    {
        isNormal = false;
        tween?.Kill();
        tween = expandToolTF.DOAnchorPos(expandStateTF, 1f);
    }
    public void SwitchState()
    {
        if (isNormal)
        {
            ToExpandState();
        }
        else
        {
            ToNormalState();
        }
    }
}
