
using UnityEngine;

public class DestroyChanceBooster : Booster
{
    private float _destroyChance;
    
    protected override void Initialize()
    {
        base.Initialize();
        
        var destroyChanceBoosterData = _boosterData as DestroyChanceBoosterData;
        
        _destroyChance = destroyChanceBoosterData.DestroyChance;
    }

    protected override void BoosterEffect(Cube cubeTrigger)
    {
        if (_destroyChance >= Random.value)
        {
            cubeTrigger.DisableCube();
        }
        base.BoosterEffect(cubeTrigger);
    }
}
