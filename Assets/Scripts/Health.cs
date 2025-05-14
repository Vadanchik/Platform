using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxValue;

    private int _currentValue;

    public int CurrentValue => _currentValue;
    public int MaxValue => _maxValue;
    public bool IsAlive => _currentValue > 0;

    public event Action<float> ValueChanged;

    private void Start()
    {
        _currentValue = _maxValue;
        ValueChanged?.Invoke(_currentValue);
    }

    public void Substract(int value)
    {
        _currentValue = Mathf.Clamp(_currentValue - Mathf.Clamp(value, 0, int.MaxValue), 0, _maxValue);
        ValueChanged?.Invoke(_currentValue);
    }

    public void Increase(int value)
    {
        _currentValue = Mathf.Clamp(_currentValue + Mathf.Clamp(value, 0, int.MaxValue), 0, _maxValue);
        ValueChanged?.Invoke(_currentValue);
    }
}
