

using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FreezeFirstOrLastBooster : Booster
{
    public static event Func<GameObject> OnFreezeFirst;
    public static event Func<GameObject> OnFreezeLast;
    private float _freezeDuration;
    
    protected override void Initialize()
    {
        base.Initialize();
        
        var freezeFirstOrLastBoosterData = _boosterData as FreezeFirstOrLastBoosterData;
        
        _freezeDuration = freezeFirstOrLastBoosterData.FreezeDuration;
    }

    protected override void BoosterEffect(Cube cubeTrigger)
    {
        FreezeFirstOrLast();
        base.BoosterEffect(cubeTrigger);
    }

    private void FreezeFirstOrLast()
    {
        var cubeToFreeze = Random.value <= 0.5 ? OnFreezeFirst?.Invoke().GetComponent<Cube>() 
            : OnFreezeLast?.Invoke().GetComponent<Cube>();
        
        cubeToFreeze.SlowDownCube(_freezeDuration);
    }
}
