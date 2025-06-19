using System;
using System.Collections;
using UnityEngine;

public class RoundSystem : MonoBehaviour
{
    public static event Action OnRoundStart;
    public static event Action OnRoundEnd;
    public static event Func<int> OnEliminateLastPlace;

    private readonly float _roundRestartDelay = 3f;
    
    [SerializeField] private TimerPresenter _timerPresenter;
    
    private int _boostersOnField;

    private void OnEnable()
    {
        CubeSpawner.OnEndPlacement += StartRound;
        BoosterSpawner.OnEndBoosterSpawn += SetOnFieldBoostersAmount;
        Booster.OnBoosterPickedUp += DecreaseOnFieldBoostersAmount;
    }

    private void OnDisable()
    {
        CubeSpawner.OnEndPlacement -= StartRound;
        BoosterSpawner.OnEndBoosterSpawn -= SetOnFieldBoostersAmount;
        Booster.OnBoosterPickedUp -= DecreaseOnFieldBoostersAmount;
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
    
    private void DecreaseOnFieldBoostersAmount(GameObject cube, Color color, string text, int number)
    {
        _boostersOnField--;
        if (_boostersOnField <= 0)
        {
            StartCoroutine(StartNewRound());
        }
    }

    private IEnumerator StartNewRound()
    {
        yield return new WaitForSeconds(_roundRestartDelay);
        if (OnEliminateLastPlace?.Invoke() <= 1)
        {
            GameEnd();
            yield break;
        }

        OnRoundEnd?.Invoke();
    }

    private void SetOnFieldBoostersAmount(int amount)
    {
        _boostersOnField = amount;
    }
    
    private void GameEnd()
    {
        Debug.Log("Game Ended");
    }
}
