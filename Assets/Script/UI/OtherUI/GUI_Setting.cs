using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GUI_Setting : MonoBehaviour , IGUI
{
    #region Variable
    [SerializeField] Button controls_btn;
    [SerializeField] Button graphic_btn;
    [SerializeField] Button audio_btn;

    [SerializeField] GameObject controls_panel;
    [SerializeField] GameObject graphic_panel;
    [SerializeField] GameObject audio_panel;


    [SerializeField] TMP_Dropdown displayDropDown;
    [SerializeField] TMP_Dropdown fpsDropDown;
    [SerializeField] GameObject music_btn, sfx_btn;
    [SerializeField] Slider musicSlider, sfxSlider;

    public List<Resolution> _settingResolution = new List<Resolution>
    {
        new Resolution {width = 2560 , height = 1440},   // QHD
        new Resolution {width = 1920 , height = 1080},  // full HD
        new Resolution {width = 1600 , height = 900},
        new Resolution {width = 1366 , height = 768},
        new Resolution {width = 1280 , height = 720},
        new Resolution {width = 1024 , height = 576},
        new Resolution {width = 854 , height = 480},
        new Resolution {width = 640 , height = 360},
    };
    public List<string> _settingfps = new List<string> { "30", "36", "45", "60", "120", "144", "Unlimit" };

    private const string Key_CurrentResolutionIndex = "ResolutionIndex"; 
    private const string Key_CurrentFpsIndex = "FpsIndex";
    #endregion

    #region Main Method
    private void Start() => Initialized();
    private void OnEnable()
    {
        GUI_Manager.AddGUI(this);
        controls_btn.onClick.AddListener(Controls_Button);
        graphic_btn.onClick.AddListener(Graphic_Button);
        audio_btn.onClick.AddListener(Audio_Button);
    }
    private void OnDisable()
    {
        GUI_Manager.RemoveGUI(this);
        controls_btn.onClick.RemoveListener(Controls_Button);
        graphic_btn.onClick.RemoveListener(Graphic_Button);
        audio_btn.onClick.RemoveListener(Audio_Button);
    }
    #endregion

    #region Resurb Method
    private void Initialized()
    {
        controls_panel.gameObject.SetActive(false);
        graphic_panel.gameObject.SetActive(false);
        audio_panel.gameObject.SetActive(false);

        List<string> option = new List<string>();
        foreach (var _resolution in _settingResolution)
        {
            var typeMode = CheckResolutionFullScreen(_resolution) ? "FC" : "WD";
            option.Add($"{_resolution.width} x {_resolution.height} {typeMode}");
        }
        var _resolutionIndex = PlayerPrefs.GetInt(Key_CurrentResolutionIndex, 1);
        var _fpsIndex = PlayerPrefs.GetInt(Key_CurrentFpsIndex, 3);
        if (displayDropDown)
        {
            displayDropDown.ClearOptions();
            displayDropDown.AddOptions(option);
            displayDropDown.value = _resolutionIndex;
            displayDropDown.RefreshShownValue();
        }
        if (fpsDropDown)
        {
            fpsDropDown.ClearOptions();
            fpsDropDown.AddOptions(_settingfps);
            fpsDropDown.value = _fpsIndex;
            fpsDropDown.RefreshShownValue();
        }

        OnValueChangeDisplayMode(_resolutionIndex);
        OnValueChangeFps(_fpsIndex);
    }
    public void GetReference(GameManager _gameManager) { }
    public void UpdateDataGUI() { }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    #endregion

    #region Button Setting
    private void Controls_Button()
    {
        controls_panel.gameObject.SetActive(true);
        graphic_panel.gameObject.SetActive(false);
        audio_panel.gameObject.SetActive(false);
    }
    private void Graphic_Button()
    {
        controls_panel.gameObject.SetActive(false);
        graphic_panel.gameObject.SetActive(true);
        audio_panel.gameObject.SetActive(false);
    }
    private void Audio_Button()
    {
        controls_panel.gameObject.SetActive(false);
        graphic_panel.gameObject.SetActive(false);
        audio_panel.gameObject.SetActive(true);
    }
    #endregion

    #region Addjust Volume 
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
    #endregion

    #region Addjust Setting - Resolution

    private bool CheckResolutionFullScreen(Resolution _resolution) => _resolution is { width: >= 1920, height: >= 1080 };
    public void OnValueChangeDisplayMode(int _index)
    {
        PlayerPrefs.SetInt(Key_CurrentResolutionIndex, _index);
        var _resolution = _settingResolution[_index];
        Screen.SetResolution(_resolution.width, _resolution.height, CheckResolutionFullScreen(_resolution));
    }
    public void OnValueChangeFps(int _index)
    {
        PlayerPrefs.SetInt(Key_CurrentFpsIndex, _index);
        var _fps = _index >= 6 ? -1 : int.Parse(_settingfps[_index]); // string -> to int
        Application.targetFrameRate = _fps;
    }
    #endregion
}
