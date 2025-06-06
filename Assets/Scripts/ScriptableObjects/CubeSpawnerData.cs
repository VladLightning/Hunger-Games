
using UnityEngine;

[CreateAssetMenu(fileName = "CubeSpawnerData", menuName = "Environment/Spawners/Cube Spawner")]
public class CubeSpawnerData : SpawnerData
{
    [SerializeField] private GameObject _cubePrefab;
    public GameObject CubePrefab => _cubePrefab;
    [SerializeField] private Material[] _materials;
    public Material[] Materials => _materials;
    [SerializeField] private CubeNamesData _cubeNamesData;
    public CubeNamesData CubeNamesData => _cubeNamesData;
    
    [SerializeField] private float _placementDelay;
    public float PlacementDelay => _placementDelay;
}
