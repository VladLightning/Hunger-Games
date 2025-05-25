
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected float _spawnDelay = 0.5f;
    [SerializeField] protected Transform[] _spawnPoints;
    
    protected List<int> RandomizeTakenSpawnPoints(int amountToTake)
    {
        var random = new System.Random();
        var takenSpawnPoints = Enumerable.Range(0, _spawnPoints.Length).OrderBy(x => random.Next()).Take(amountToTake).ToList();
        return takenSpawnPoints;
    }
}
