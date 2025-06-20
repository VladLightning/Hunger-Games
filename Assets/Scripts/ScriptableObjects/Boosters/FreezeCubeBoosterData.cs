using UnityEngine;
[CreateAssetMenu(fileName = "BoosterData", menuName = "Boosters/Freeze", order = 4)]

public class FreezeCubeBoosterData : BoosterData
{
    [SerializeField] private float _freezeDuration;
    public float FreezeDuration => _freezeDuration;
}
