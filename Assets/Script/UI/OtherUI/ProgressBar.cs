using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider mainProgressSlider;
    public Slider backProgressSlider;

    public TextMeshProUGUI progressText;
    public bool ShowText;

    private float mainDuration = .25f;
    private float backDuration = .8f;
    private Tween mainProgressTween, backProgressTween;

    private void OnEnable()
    {
        if (!ShowText || !progressText) return;
        mainProgressSlider.onValueChanged.AddListener(SliderChangeValue);
    }
    private void OnDisable()
    {
        if (!ShowText || !progressText) return;
        mainProgressSlider.onValueChanged.RemoveListener(SliderChangeValue);
    }
    public void OnInitValue(int _currentValue, int _maxValue)
    {
        mainProgressSlider.minValue = 0;
        mainProgressSlider.maxValue = _maxValue;
        mainProgressSlider.value = _currentValue;

        backProgressSlider.minValue = 0;
        backProgressSlider.maxValue = _maxValue;
        backProgressSlider.value = _currentValue;
    }
    public void OnCurrentValueChange(int _currentValue, int _maxValue)
    {
        _currentValue = (int)Mathf.Clamp(_currentValue, mainProgressSlider.minValue, mainProgressSlider.maxValue);

        mainProgressTween?.Kill();
        mainProgressTween = mainProgressSlider.DOValue(_currentValue, mainDuration);

        backProgressTween?.Kill();
        backProgressTween = backProgressSlider.DOValue(_currentValue, backDuration);

        if (!ShowText || !progressText) return;
        SliderChangeValue(_currentValue);

    }
    public void OnMaxValueChange(int _maxValue)
    {
        mainProgressSlider.maxValue = _maxValue;
        backProgressSlider.maxValue = _maxValue;

        if (!ShowText || !progressText) return;
        SliderChangeValue(mainProgressSlider.value);
    }

    private void SliderChangeValue(float _value) => progressText.text = $"{Mathf.CeilToInt(_value)} / {mainProgressSlider.maxValue}";

}