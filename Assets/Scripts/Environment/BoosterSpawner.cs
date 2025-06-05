using System.Collections;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class BoosterSpawner : Spawner
{
    private SerializedDictionary<GameObject, int> _boosters;
    private int _boostersToSpawnAmount;

    protected override void Initialize()
    {
        base.Initialize();
        
        var boosterSpawnerData = _spawnerData as BoosterSpawnerData;
        
        _boosters = boosterSpawnerData.Boosters;
        _boostersToSpawnAmount = boosterSpawnerData.BoostersToSpawn;
    }
    
    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var takenSpawnPoints = RandomizeNonRepeatingListValues(_spawnPoints.Length,_boostersToSpawnAmount);

        foreach (var spawnPoint in takenSpawnPoints)
        {
            yield return new WaitForSeconds(_spawnDelay);
            Instantiate(GetRandomBooster(), _spawnPoints[spawnPoint].position, _spawnPoints[spawnPoint].rotation);
        }
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
