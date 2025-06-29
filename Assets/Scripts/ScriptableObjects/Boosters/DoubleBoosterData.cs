
using UnityEngine;
[CreateAssetMenu(fileName = "BoosterData", menuName = "Boosters/Double", order = 8)]
public class DoubleBoosterData : BoosterData
{
    [SerializeField] private int _noExtraBoosterFoundScore;
    public int NoExtraBoosterFoundScore => _noExtraBoosterFoundScore;
}
