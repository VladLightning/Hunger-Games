using System;
using System.Collections;
using UnityEngine;

public class RoundSystem : MonoBehaviour
{
    public static event Action OnRoundStart;
    public static event Action OnRoundEnd;
    public static event Action OnEliminateLastPlace;
    public static event Func<LeaderboardPresenter.LeaderboardData> OnGetFirstPlaceStats;
    public static event Func<int> OnCheckOnFieldCubesAmount;

    private readonly float _roundRestartDelay = 3f;
    
    [SerializeField] private TimerPresenter _timerPresenter;
    
    private int _boostersOnField;

    private void OnEnable()
    {
        Cube.OnCheckGameEnd += CheckGameEnd;
        
        CubeSpawner.OnEndPlacement += StartRound;
        BoosterSpawner.OnEndBoosterSpawn += SetOnFieldBoostersAmount;
        Booster.OnBoosterPickedUp += DecreaseOnFieldBoostersAmount;
    }

    private void OnDisable()
    {
        Cube.OnCheckGameEnd -= CheckGameEnd;
        
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
        OnEliminateLastPlace?.Invoke();
        if (OnCheckOnFieldCubesAmount?.Invoke() <= 1)
        {
            OnEliminateLastPlace?.Invoke();
            GameEnd();
            yield break;
        }

        OnRoundEnd?.Invoke();
    }

    private void SetOnFieldBoostersAmount(int amount)
    {
        _boostersOnField = amount;
    }

    private void CheckGameEnd()
    {
        if (OnCheckOnFieldCubesAmount?.Invoke() == 0)
        {
            GameEnd();
        }
    }
    
    private void GameEnd()
    {
        var firstPlaceStats = OnGetFirstPlaceStats?.Invoke();
        if (firstPlaceStats == null)
        {
            throw new NullReferenceException("OnGetFirstPlaceStats returned null");
        }
        var cubeName = firstPlaceStats.Name;
        var cubeScore = firstPlaceStats.Score;
        string gameEndMessage = $"Cube {cubeName} won with a total of {cubeScore} points";
        
        Debug.Log(gameEndMessage);
    }
}
