using System.Collections;
using System.Linq;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private readonly float _spawnDelay = 0.5f;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Material[] _colors;
    
    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var random = new System.Random();
        var takenSpawnPoints = Enumerable.Range(0, _spawnPoints.Length).OrderBy(x => random.Next()).Take(_colors.Length).ToList();
        
        for(int i = 0; i < takenSpawnPoints.Count; i++)
        {
            yield return new WaitForSeconds(_spawnDelay);
            var renderer = Instantiate(_cubePrefab, _spawnPoints[takenSpawnPoints[i]].position, _spawnPoints[takenSpawnPoints[i]].rotation).GetComponent<Renderer>();
            renderer.material = _colors[i];
        }
    }
}
