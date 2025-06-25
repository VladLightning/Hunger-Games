
using System.Collections.Generic;
using UnityEngine;

public class SlowDownRandomCubeBooster : Booster
{
    private readonly float _boosterRange = 100f;
    
    private float _slowDownDuration;
    private float _slowDownCoefficient;

    protected override void Initialize()
    {
        base.Initialize();
        
        var slowDownRandomCubeBoosterData = _boosterData as SlowDownRandomCubeBoosterData;
        
        _slowDownDuration = slowDownRandomCubeBoosterData.SlowDownDuration;
        _slowDownCoefficient = slowDownRandomCubeBoosterData.SlowDownCoefficient;
    }

    protected override void BoosterEffect(Cube cubeTrigger)
    {
        SlowDownRandomCube(cubeTrigger);
        base.BoosterEffect(cubeTrigger);
    }

    private void SlowDownRandomCube(Cube cubeTrigger)
    {
        var cubes = Physics.OverlapSphere(transform.position, _boosterRange, LayerMask.GetMask("Cube"));

        while(true)
        {
            var randomCube = cubes[Random.Range(0, cubes.Length)].GetComponent<Cube>();
            if (randomCube != cubeTrigger)
            {
                randomCube.SlowDownCube(_slowDownDuration, _slowDownCoefficient);
                break;
            }
        }
    }
}
