using System;
using System.Collections;
using UnityEngine;

public class RoundSystem : MonoBehaviour
{
    public static event Action OnRoundStart;
    public static event Action OnRoundEnd;

    private readonly float _roundRestartDelay = 3f;
    
    [SerializeField] private TimerPresenter _timerPresenter;
    
    private int _boostersOnField;

    private void OnEnable()
    {
        CubeSpawner.OnEndPlacement += StartRound;
        BoosterSpawner.OnEndBoosterSpawn += SetOnFieldBoostersAmount;
        Cube.OnBoosterPickedUp += DecreaseOnFieldBoostersAmount;
    }

    private void OnDisable()
    {
        CubeSpawner.OnEndPlacement -= StartRound;
        BoosterSpawner.OnEndBoosterSpawn -= SetOnFieldBoostersAmount;
        Cube.OnBoosterPickedUp -= DecreaseOnFieldBoostersAmount;
    }

    private void StartRound()
    {
        StartCoroutine(RoundStart());
    }

    private IEnumerator RoundStart()
    {
        yield return StartCoroutine(_timerPresenter.TimerCounter());
        OnRoundStart?.Invoke();
    }

    private IEnumerator RoundRestart()
    {
        yield return new WaitForSeconds(_roundRestartDelay);
        OnRoundEnd?.Invoke();
    }
    
    private void DecreaseOnFieldBoostersAmount(Color color, string text, int number)
    {
        _boostersOnField--;
        if (_boostersOnField <= 0)
        {
           StartCoroutine(RoundRestart());
        }
    }

    private void SetOnFieldBoostersAmount(int amount)
    {
        _boostersOnField = amount;
    }
}
