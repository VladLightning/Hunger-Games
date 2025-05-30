using UnityEngine;

public abstract class SpawnerData : ScriptableObject
{
    [SerializeField] private float _spawnDelay;
    public float SpawnDelay => _spawnDelay;
}
