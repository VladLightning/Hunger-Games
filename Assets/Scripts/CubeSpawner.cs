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

        int colorIndex = 0;
        
        foreach (var spawnPoint in takenSpawnPoints)
        {
            yield return new WaitForSeconds(_spawnDelay);
            var renderer = Instantiate(_cubePrefab, _spawnPoints[spawnPoint].position, _spawnPoints[spawnPoint].rotation).GetComponent<Renderer>();
            renderer.material = _colors[colorIndex];
            colorIndex++;
        }
    }
}
