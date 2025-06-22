
using System.Collections.Generic;
using UnityEngine;

public class SlowDownRandomCubeBooster : Booster
{
    private readonly float _boosterRange = 100f;
    
    private float _slowDownDuration;
    private float _slowDownCoefficient;
    private LayerMask _layerMask;

    protected override void Initialize()
    {
        base.Initialize();
        
        var slowDownRandomCubeBoosterData = _boosterData as SlowDownRandomCubeBoosterData;
        
        _slowDownDuration = slowDownRandomCubeBoosterData.SlowDownDuration;
        _slowDownCoefficient = slowDownRandomCubeBoosterData.SlowDownCoefficient;
        _layerMask = slowDownRandomCubeBoosterData.LayerMask;
    }

    protected override void BoosterEffect(Cube cubeTrigger)
    {
        SlowDownRandomCube(cubeTrigger);
        base.BoosterEffect(cubeTrigger);
    }

    private void SlowDownRandomCube(Cube cubeTrigger)
    {
        var cubes = Physics.OverlapSphere(transform.position, _boosterRange, _layerMask);
        var activeCubes = new List<Cube>();

        foreach (var cube in cubes)
        {
            var cubeComponent = cube.GetComponent<Cube>();
            if (cube.gameObject.activeSelf && cubeComponent != cubeTrigger)
            {
                activeCubes.Add(cubeComponent);
            }
        }
        
        activeCubes[Random.Range(0, activeCubes.Count)].FreezeCube(_slowDownDuration ,_slowDownCoefficient);
    }
}
