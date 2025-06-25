

using System;
using Random = UnityEngine.Random;

public class FreezeFirstOrLastBooster : Booster
{
    public static event Func<LeaderboardPresenter> OnFreezeFirstOrLast;
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
        var leaderboardPresenter = OnFreezeFirstOrLast?.Invoke();
        var cubeToFreeze = Random.value <= 0.5 ? leaderboardPresenter.GetFirstPlace().GetComponent<Cube>() 
            : leaderboardPresenter.GetLastPlace().GetComponent<Cube>();
        
        cubeToFreeze.SlowDownCube(_freezeDuration);
    }
}
