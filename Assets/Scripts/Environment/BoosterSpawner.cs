using System;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoosterSpawner : Spawner
{
    public static event Action<int> OnEndBoosterSpawn;
    private SerializedDictionary<GameObject, int> _boosters;
    private int _boostersToSpawnAmount;
    
    protected override void Initialize()
    {
        var boosterSpawnerData = _spawnerData as BoosterSpawnerData;
        
        _boosters = boosterSpawnerData.Boosters;
        _boostersToSpawnAmount = boosterSpawnerData.BoostersToSpawn;
    }

    private void OnEnable()
    {
        RoundSystem.OnRoundEnd += Spawn;
    }

    private void OnDisable()
    {
        RoundSystem.OnRoundEnd -= Spawn;
    }

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        var takenSpawnPoints = RandomizeNonRepeatingListValues(_spawnPoints.Length,_boostersToSpawnAmount);

        foreach (var spawnPoint in takenSpawnPoints)
        {
            Instantiate(GetRandomBooster(), _spawnPoints[spawnPoint].position, _spawnPoints[spawnPoint].rotation);
        }

        OnEndBoosterSpawn?.Invoke(_boostersToSpawnAmount);
    }

    private GameObject GetRandomBooster()
    {
        int totalWeight = _boosters.Values.Sum();
        int roll = Random.Range(0, totalWeight);
        int compareWeightCounter = 0;

        foreach (var item in _boosters)
        {
            compareWeightCounter += item.Value;
            if (compareWeightCounter >= roll)
            {
                return item.Key;
            }
        }
        return _boosters.First().Key;
    }
}
