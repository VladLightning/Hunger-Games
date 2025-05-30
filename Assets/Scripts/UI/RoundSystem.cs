using System;
using System.Collections;
using UnityEngine;

public class RoundSystem : MonoBehaviour
{
    public static event Action OnRoundStart;
    
    [SerializeField] private TimerPresenter _timerPresenter;

    private void OnEnable()
    {
        CubeSpawner.OnEndSpawn += StartRound;
    }

    private void OnDisable()
    {
        CubeSpawner.OnEndSpawn -= StartRound;
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
}
