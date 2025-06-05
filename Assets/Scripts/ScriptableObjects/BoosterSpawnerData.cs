using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "BoosterSpawnerData", menuName = "Environment/Spawners/Booster Spawner")]
public class BoosterSpawnerData : SpawnerData
{
    [SerializeField] private SerializedDictionary<GameObject, int> _boosters;
    public SerializedDictionary<GameObject, int> Boosters => _boosters;
    
    [Range(0, 13)]
    [SerializeField] private int _boostersToSpawn;
    public int BoostersToSpawn => _boostersToSpawn;
}
