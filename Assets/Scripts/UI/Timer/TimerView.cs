using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    private TMP_Text _timerDisplay;

    private void Awake()
    {
        _timerDisplay = GetComponent<TMP_Text>();
    }

    public void TimerDisplaySetActive(bool value)
    {
        gameObject.SetActive(value);
    }
    
    public void UpdateTimerDisplay(float seconds)
    {
        _timerDisplay.text = $"{seconds:0.0}";
    }
}
