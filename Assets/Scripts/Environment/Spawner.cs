
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

    protected List<int> RandomizeNonRepeatingListValues(int upperLimit, int amountToTake)
    {
        var random = new System.Random();
        var takenValues = Enumerable.Range(0, upperLimit).OrderBy(x => random.Next()).Take(amountToTake).ToList();
        return takenValues;
    }
}
