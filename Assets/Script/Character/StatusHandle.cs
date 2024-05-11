using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class StatusHandle 
{
    public event Action<int, int> OnValueChangeEvent;
    public event Action<int> OnMaxValueChangeEvent;
    public event Action<int, int> OnInitValueEvent;
    public event Action OnDieEvent;
    public int currentValue { get; set; }
    public int maxValue { get; set; }

    public void InitValue(int _currentValue, int _maxValue)
    {
        currentValue = _currentValue;
        maxValue = _maxValue;
        OnInitValueEvent?.Invoke(_currentValue, _maxValue);
        OnValueChangeEvent?.Invoke((int)_currentValue, (int)_maxValue);
    }
    public void UpdateValue(int _maxValue)
    {
        maxValue = _maxValue;
        OnMaxValueChangeEvent?.Invoke(_maxValue);
    }

    public void Increase(int _value)
    {
        if (_value <= 0)
            return;
        currentValue = Mathf.Clamp(currentValue + _value, 0, maxValue);
        OnValueChangeEvent?.Invoke(currentValue, maxValue);
    }
    public void Decrease(int _value)
    {
        if (_value <= 0)
            return;
        currentValue = Mathf.Clamp(currentValue - _value, 0, maxValue);
        OnValueChangeEvent?.Invoke(currentValue, maxValue);
        if (currentValue < 0)
            OnDieEvent?.Invoke();
    }
}
