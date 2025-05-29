using System.Collections;
using UnityEngine;

public class TimerPresenter : MonoBehaviour
{
    [SerializeField] private float _timerDuration;
    [SerializeField] private TimerView _timerView;

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
