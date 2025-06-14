
using UnityEngine;
[CreateAssetMenu(fileName = "BoosterData", menuName = "Boosters/Default", order = 1)]
public class BoosterData : ScriptableObject
{
    [SerializeField] private int _score;
    public int Score => _score;
}
