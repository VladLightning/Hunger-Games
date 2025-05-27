using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timerDuration;
    private TimerView _timerView;
    
    private Coroutine _timer;
    public Coroutine WaitTimer => _timer;

    private void Awake()
    {
        _timerView = GetComponent<TimerView>();
    }

    public void StartTimerCounter()
    {
        _timerView.TimerDisplaySetActive(true);
        _timer = StartCoroutine(TimerCounter());
    }

    private IEnumerator TimerCounter()
    {
        while (_timerDuration > 0)
        {
            yield return new WaitForFixedUpdate();
            _timerDuration -= Time.deltaTime;
            _timerView.UpdateTimerDisplay(_timerDuration);
        }
        _timerView.TimerDisplaySetActive(false);
    }
}
