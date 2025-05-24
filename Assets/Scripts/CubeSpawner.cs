using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner
{
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
            var renderer = Instantiate(_cubePrefab, _spawnPoints[takenSpawnPoints[i]].position, _spawnPoints[takenSpawnPoints[i]].rotation).GetComponent<Renderer>();
            renderer.material = _colors[i];
        }
    }
}
