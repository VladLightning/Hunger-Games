using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner
{
    public static event Action OnEndSpawn;
    
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Material[] _colors;
    
    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var takenSpawnPoints = RandomizeTakenSpawnPoints(_colors.Length);
        
        for(int i = 0; i < takenSpawnPoints.Count; i++)
        {
            yield return new WaitForSeconds(_spawnDelay);
            var cube = Instantiate(_cubePrefab, _spawnPoints[takenSpawnPoints[i]].position, _spawnPoints[takenSpawnPoints[i]].rotation).GetComponent<Cube>();
            
            cube.Initialize(_colors[i], i+1);
        }
        
        OnEndSpawn?.Invoke();
    }
}
