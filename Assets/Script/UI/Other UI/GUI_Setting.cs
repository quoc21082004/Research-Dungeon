﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUI_Setting : MonoBehaviour
{
    public GameObject music_btn, sfx_btn;
    public Slider musicSlider, sfxSlider;
    private void OnEnable()
    {
        InputManager.playerInput.Disable();
        GUI_Input.playerInput.UI.Disable();
    }
    private void OnDisable()
    {
        InputManager.playerInput.Enable();
        GUI_Input.playerInput.UI.Enable();
    }
    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
        if (!AudioManager.instance.musicSource.mute)
            music_btn.gameObject.GetComponent<Image>().color = Color.white;
        else
            music_btn.gameObject.GetComponent<Image>().color = Color.red;
    }
    public void ToggleSfx()
    {
        AudioManager.instance.ToggleSfx();
        if (!AudioManager.instance.sfxSource.mute)
            sfx_btn.gameObject.GetComponent<Image>().color = Color.white;
        else
            sfx_btn.gameObject.GetComponent<Image>().color = Color.red;
    }
    public void MusicVolume() => AudioManager.instance.MusicVolume(musicSlider.value);
    public void SfxVolume() => AudioManager.instance.SfxVolume(sfxSlider.value);

}
