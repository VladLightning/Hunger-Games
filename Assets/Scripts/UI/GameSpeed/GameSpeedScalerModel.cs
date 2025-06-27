
using System;
using UnityEngine;

public class GameSpeedScalerModel
{
    public static event Action<float> OnTimeScaleChanged;
    
    public void ChangeTimeScale(float timeScaleChangeFactor)
    {
        Time.timeScale += timeScaleChangeFactor;
        if (Time.timeScale <= 1)
        {
            Time.timeScale = 1;
        }
        OnTimeScaleChanged?.Invoke(Time.timeScale);
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
    }
}
