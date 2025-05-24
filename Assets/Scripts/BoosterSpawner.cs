using System.Collections;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class BoosterSpawner : Spawner
{
    [SerializeField] private SerializedDictionary<GameObject, int> _boosters;
    
    [SerializeField] private int _boostersToSpawnAmount;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var takenSpawnPoints = RandomizeTakenSpawnPoints(_boostersToSpawnAmount);

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
        int cumulative = 0;

        foreach (var item in _boosters)
        {
            cumulative += item.Value;
            if (cumulative >= roll)
            {
                return item.Key;
            }
        }
        return _boosters.First().Key;
    }
}
