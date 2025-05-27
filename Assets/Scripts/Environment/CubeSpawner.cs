using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Material[] _colors;
    [SerializeField] private Timer _timer;
    
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
            var movement = cube.GetComponent<Movement>();
            
            renderer.material = _colors[i];
            movement.SetTimer(_timer);
        }
    }
}
