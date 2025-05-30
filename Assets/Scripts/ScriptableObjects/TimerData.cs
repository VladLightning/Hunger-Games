using UnityEngine;
[CreateAssetMenu(fileName = "TimerData", menuName = "Environment/Timer")]

public class TimerData : ScriptableObject
{
    [SerializeField] private float _timerDuration;
    public float TimerDuration => _timerDuration;
}
