using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : Spawner
{
    public static event Action OnEndPlacement;

    [SerializeField] private LeaderboardPresenter _leaderboardPresenter;
    
    private GameObject _cubePrefab;
    private Material[] _materials;
    
    private CubeNamesData _cubeNamesData;
    
    private List<GameObject> _onFieldCubes = new List<GameObject>();
    
    private float _placementDelay;
    
    protected override void Initialize()
    {
        var cubeSpawnerData = _spawnerData as CubeSpawnerData;
        
        _cubePrefab = cubeSpawnerData.CubePrefab;
        _materials = cubeSpawnerData.Materials;
        _cubeNamesData = cubeSpawnerData.CubeNamesData;
        _placementDelay = cubeSpawnerData.PlacementDelay;
    }

    private void OnEnable()
    {
        RoundSystem.OnRoundEnd += StartSetCubePositions;
    }

    private void OnDisable()
    {
        RoundSystem.OnRoundEnd -= StartSetCubePositions;
    }

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        var takenNames = RandomizeNonRepeatingListValues(_cubeNamesData.CubeNames.Count,_materials.Length);
        
        _leaderboardPresenter.SpawnDisplays(takenNames.Count);
        
        for(int i = 0; i < takenNames.Count; i++)
        {
            ExecuteSpawn(i, _cubeNamesData.CubeNames[takenNames[i]]);
        }
        StartSetCubePositions();
    }

    private void ExecuteSpawn(int index, string takenName)
    {
        var cube = Instantiate(_cubePrefab).GetComponent<Cube>();
        _leaderboardPresenter.AddLeaderboardElement(cube.gameObject, _materials[index].color, takenName);
        cube.Initialize(_materials[index], takenName,index+1);
        
        _onFieldCubes.Add(cube.gameObject);
    }

    private void StartSetCubePositions()
    {
        StartCoroutine(SetCubePositions());
    }

    private IEnumerator SetCubePositions()
    {
        foreach (var onFieldCube in _onFieldCubes)
        {
            if (onFieldCube == null)
            {
                //todo remove null element
                continue;
            }
            onFieldCube.SetActive(false);
        }
        
        var takenSpawnPoints = RandomizeNonRepeatingListValues(_spawnPoints.Length,_materials.Length);
        
        int index = 0;
        foreach (var cube in _onFieldCubes)
        {
            if (cube == null)
            {
                //todo remove null element
                continue;
            }
            yield return new WaitForSeconds(_placementDelay);
            cube.transform.position = _spawnPoints[takenSpawnPoints[index]].position;
            cube.transform.rotation = Quaternion.identity;
            cube.SetActive(true);
            index++;
        }
        OnEndPlacement?.Invoke();
    }
}
