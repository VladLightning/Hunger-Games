using System.Collections;
using UnityEngine;

public class TimerPresenter : MonoBehaviour
{
    [SerializeField] private TimerData _timerData;
    [SerializeField] private TimerView _timerView;
    
    private float _timerDuration;
    
    private void Initialize()
    {
        _timerDuration = _timerData.TimerDuration;
    }
    
    private void Start()
    {
        Initialize();
    }
    
    public IEnumerator TimerCounter()
    {
        _timerView.TimerDisplaySetActive(true);
        while (_timerDuration > 0)
        {
            _timerView.UpdateTimerDisplay(_timerDuration);
            _timerDuration -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        _timerView.TimerDisplaySetActive(false);
    }
}
