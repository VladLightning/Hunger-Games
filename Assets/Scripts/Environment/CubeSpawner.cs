using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : Spawner
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Material[] _colors;
    [SerializeField] private RoundSystem _roundSystem;

    private List<Movement> _cubesMovements = new List<Movement>();
    public List<Movement> CubesMovements => _cubesMovements;
    
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
            var cube = Instantiate(_cubePrefab, _spawnPoints[takenSpawnPoints[i]].position, _spawnPoints[takenSpawnPoints[i]].rotation);
            
            var renderer = cube.GetComponent<Renderer>();
            renderer.material = _colors[i];
            
            _cubesMovements.Add(cube.GetComponent<Movement>());
        }
        
        _roundSystem.StartRound(_cubesMovements);
    }
}
