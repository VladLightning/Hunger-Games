using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeSpawner : Spawner
{
    public static event Action OnEndPlacement;

    [SerializeField] private LeaderboardPresenter _leaderboardPresenter;
    
    private GameObject _cubePrefab;
    private Material[] _materials;
    
    private CubeNamesData _cubeNamesData;
    
    private Dictionary<GameObject, int> _onFieldCubes = new Dictionary<GameObject, int>();
    
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
        RoundSystem.OnEliminateLastPlace += EliminateLastPlace;
        RoundSystem.OnOneCubeLeft += CheckIsOneCubeLeft;
        Cube.OnScoreChanged += UpdateCubeScore;
    }

    private void OnDisable()
    {
        RoundSystem.OnRoundEnd -= StartSetCubePositions;
        RoundSystem.OnEliminateLastPlace -= EliminateLastPlace;
        RoundSystem.OnOneCubeLeft -= CheckIsOneCubeLeft;
        Cube.OnScoreChanged -= UpdateCubeScore;
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
        _leaderboardPresenter.AddLeaderboardElement(_materials[index].color, takenName);
        cube.Initialize(_materials[index], takenName,index+1);
        
        _onFieldCubes.Add(cube.gameObject, 0);
    }

    private void StartSetCubePositions()
    {
        StartCoroutine(SetCubePositions());
    }

    private IEnumerator SetCubePositions()
    {
        foreach (var onFieldCube in _onFieldCubes.Keys)
        {
            onFieldCube.SetActive(false);
        }
        
        var takenSpawnPoints = RandomizeNonRepeatingListValues(_spawnPoints.Length,_materials.Length);
        
        int index = 0;
        foreach (var cube in _onFieldCubes.Keys)
        {
            yield return new WaitForSeconds(_placementDelay);
            cube.transform.position = _spawnPoints[takenSpawnPoints[index]].position;
            cube.transform.rotation = Quaternion.identity;
            cube.SetActive(true);
            index++;
        }
        OnEndPlacement?.Invoke();
    }
    
    private void UpdateCubeScore(GameObject cube, int score)
    {
        _onFieldCubes[cube] = score;
    }
    
    private void EliminateLastPlace()
    {
        var lastPlaceCube = _onFieldCubes.OrderByDescending(x => x.Value).Last().Key;
        
        _onFieldCubes.Remove(lastPlaceCube);
        
        lastPlaceCube.GetComponent<Cube>().DestroyCube();
    }

    private bool CheckIsOneCubeLeft()
    {
        return _onFieldCubes.Count == 1;
    }
}
