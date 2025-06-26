
using System;
using TMPro;
using UnityEngine;

public class GameSpeedScalerView : MonoBehaviour
{
    public static event Action<float> OnChangeTimeScale;
    [SerializeField] private TMP_Text _timeScaleDisplay;

    public void UpdateTimeScaleDisplay(float timeScale)
    {
        _timeScaleDisplay.text = timeScale.ToString("0.00");
    }

    public void OnClick(float timeScaleChangeFactor)
    {
        OnChangeTimeScale?.Invoke(timeScaleChangeFactor);
    }
}
