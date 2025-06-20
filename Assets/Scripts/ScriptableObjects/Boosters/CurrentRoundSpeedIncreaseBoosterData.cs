
using UnityEngine;
[CreateAssetMenu(fileName = "BoosterData", menuName = "Boosters/CurrentRoundSpeedIncrease", order = 3)]

public class CurrentRoundSpeedIncreaseBoosterData : BoosterData
{
    [SerializeField] private float _speedIncreaseCoefficient;
    public float SpeedIncreaseCoefficient => _speedIncreaseCoefficient;
}
