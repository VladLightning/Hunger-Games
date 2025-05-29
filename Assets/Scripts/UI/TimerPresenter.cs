using System.Collections;
using UnityEngine;

public class TimerPresenter : MonoBehaviour
{
    [SerializeField] private float _timerDuration;
    private TimerView _timerView;
    
    private void Awake()
    {
        _timerView = GetComponent<TimerView>();
    }

    public IEnumerator TimerCounter()
    {
        _timerView.TimerDisplaySetActive(true);
        while (_timerDuration > 0)
        {
            yield return new WaitForFixedUpdate();
            _timerDuration -= Time.deltaTime;
            _timerView.UpdateTimerDisplay(_timerDuration);
        }
        _timerView.TimerDisplaySetActive(false);
    }
}
