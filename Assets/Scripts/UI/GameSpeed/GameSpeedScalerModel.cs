
using System;
using UnityEngine;

public class GameSpeedScalerModel
{
    public static event Action<float> OnTimeScaleChanged;

    private float _currentTimeScale = 1f;
    public float CurrentTimeScale => _currentTimeScale;

    public void ChangeTimeScale(float timeScaleChangeFactor)
    {
        _currentTimeScale += timeScaleChangeFactor;
        if (_currentTimeScale <= 1)
        {
            _currentTimeScale = 1;
        }
        Time.timeScale = _currentTimeScale;
        OnTimeScaleChanged?.Invoke(_currentTimeScale);
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
    }
}
