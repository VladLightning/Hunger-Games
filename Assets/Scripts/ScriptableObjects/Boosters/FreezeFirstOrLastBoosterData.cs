using UnityEngine;
[CreateAssetMenu(fileName = "BoosterData", menuName = "Boosters/FreezeFirstOrLast", order = 7)]

public class FreezeFirstOrLastBoosterData : BoosterData
{
    [SerializeField] private float _freezeDuration;
    public float FreezeDuration => _freezeDuration;
}
