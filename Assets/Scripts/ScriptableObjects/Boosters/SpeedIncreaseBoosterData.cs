
using UnityEngine;
[CreateAssetMenu(fileName = "BoosterData", menuName = "Boosters/SpeedIncrease", order = 3)]
public class SpeedIncreaseBoosterData : BoosterData
{
    [SerializeField] private float _speedIncreaseCoefficient;
    public float SpeedIncreaseCoefficient => _speedIncreaseCoefficient;
}
