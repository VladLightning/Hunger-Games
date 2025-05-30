using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner
{
    public static event Action OnEndSpawn;
    
    private GameObject _cubePrefab;
    private Material[] _materials;
    
    protected override void Initialize()
    {
        base.Initialize();
        
        var cubeSpawnerData = _spawnerData as CubeSpawnerData;
        
        _cubePrefab = cubeSpawnerData.CubePrefab;
        _materials = cubeSpawnerData.Materials;
    }
    
    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var takenSpawnPoints = RandomizeTakenSpawnPoints(_materials.Length);
        
        for(int i = 0; i < takenSpawnPoints.Count; i++)
        {
            yield return new WaitForSeconds(_spawnDelay);
            var cube = Instantiate(_cubePrefab, _spawnPoints[takenSpawnPoints[i]].position, _spawnPoints[takenSpawnPoints[i]].rotation).GetComponent<Cube>();
            
            cube.Initialize(_materials[i], i+1);
        }
        
        OnEndSpawn?.Invoke();
    }
}
