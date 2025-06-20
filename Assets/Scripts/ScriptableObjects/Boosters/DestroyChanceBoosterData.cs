
using UnityEngine;
[CreateAssetMenu(fileName = "BoosterData", menuName = "Boosters/DestroyChance", order = 2)]
public class DestroyChanceBoosterData : BoosterData
{
    [SerializeField] private float _destroyChance;
    public float DestroyChance => _destroyChance;
}
