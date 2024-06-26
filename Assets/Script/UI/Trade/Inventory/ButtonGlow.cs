﻿using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonGlow : MonoBehaviour , ISelectHandler , IDeselectHandler
{
    public GameObject buttonGlowImg;
    public void OnDisable()
    {
        buttonGlowImg.gameObject.SetActive(false);   
    }
    public void OnSelect(BaseEventData eventData)
    {
        buttonGlowImg.gameObject.SetActive(true);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        buttonGlowImg.gameObject.SetActive(false);
    }
}
