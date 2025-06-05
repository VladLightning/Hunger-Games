using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeSpawner : Spawner
{
    public static event Action<int> OnStartSpawn;
    public static event Action OnEndSpawn;

    [SerializeField] private LeaderboardPresenter _leaderboardPresenter;
    
    private GameObject _cubePrefab;
    private Material[] _materials;
    
    private CubeNamesData _cubeNamesData;
    
    protected override void Initialize()
    {
        base.Initialize();
        
        var cubeSpawnerData = _spawnerData as CubeSpawnerData;
        
        _cubePrefab = cubeSpawnerData.CubePrefab;
        _materials = cubeSpawnerData.Materials;
        _cubeNamesData = cubeSpawnerData.CubeNamesData;
    }
    
    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var takenSpawnPoints = RandomizeTakenSpawnPoints(_materials.Length);
        var takenNames = RandomizeNames(_materials.Length);
        
        _leaderboardPresenter.SpawnDisplays(takenSpawnPoints.Count);
        
        for(int i = 0; i < takenSpawnPoints.Count; i++)
        {
            yield return new WaitForSeconds(_spawnDelay);
            var cube = Instantiate(_cubePrefab, _spawnPoints[takenSpawnPoints[i]].position, _spawnPoints[takenSpawnPoints[i]].rotation).GetComponent<Cube>();
            _leaderboardPresenter.AddLeaderboardElement(_materials[i].color, _cubeNamesData.CubeNames[takenNames[i]]);
            cube.Initialize(_materials[i], _cubeNamesData.CubeNames[takenNames[i]],i+1);
        }
        
        OnEndSpawn?.Invoke();
    }

    private List<int> RandomizeNames(int amount)
    {
        var random = new System.Random();
        var takenNames = Enumerable.Range(0, _cubeNamesData.CubeNames.Count).OrderBy(x => random.Next()).Take(amount).ToList();
        return takenNames;
    }
}
