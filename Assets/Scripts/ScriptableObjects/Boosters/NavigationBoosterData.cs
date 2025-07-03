using UnityEngine;
[CreateAssetMenu(fileName = "BoosterData", menuName = "Boosters/Navigation", order = 9)]
public class NavigationBoosterData : BoosterData
{
    [SerializeField] private float _navigatedBoosterSpeed;
    public float NavigatedBoosterSpeed => _navigatedBoosterSpeed;
}
