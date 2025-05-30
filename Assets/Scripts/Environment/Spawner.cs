
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected SpawnerData _spawnerData;
    [SerializeField] protected Transform[] _spawnPoints;
    protected float _spawnDelay;
    protected virtual void Initialize()
    {
        _spawnDelay = _spawnerData.SpawnDelay;
    }
    
    private void Awake()
    {
        Initialize();
    }

    protected List<int> RandomizeTakenSpawnPoints(int amountToTake)
    {
        var random = new System.Random();
        var takenSpawnPoints = Enumerable.Range(0, _spawnPoints.Length).OrderBy(x => random.Next()).Take(amountToTake).ToList();
        return takenSpawnPoints;
    }
}
