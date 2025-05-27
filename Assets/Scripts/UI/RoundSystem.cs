using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSystem : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    private List<Movement> _movements;
    
    public void StartRound(List<Movement> movements)
    {
        _movements = movements;
        StartCoroutine(RoundStart());
    }

    private IEnumerator RoundStart()
    {
        _timer.StartTimerCounter();
        yield return _timer.WaitTimer;

        foreach (var movement in _movements)
        {
            movement.StartMoving();
        }
    }
}
