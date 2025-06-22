using UnityEngine;
[CreateAssetMenu(fileName = "BoosterData", menuName = "Boosters/SlowDownRandomCube", order = 6)]
public class SlowDownRandomCubeBoosterData : BoosterData
{
    [SerializeField] private float _slowDownDuration;
    public float SlowDownDuration => _slowDownDuration;
    [SerializeField] private float _slowDownCoefficient;
    public float SlowDownCoefficient => _slowDownCoefficient;
    [SerializeField] private LayerMask _layerMask;
    public LayerMask LayerMask => _layerMask;
}
