
public class FreezeCubeBooster : Booster
{
    private float _freezeDuration;

    protected override void Initialize()
    {
        base.Initialize();
        
        var freezeCubeBoosterData = _boosterData as FreezeCubeBoosterData;
        
        _freezeDuration = freezeCubeBoosterData.FreezeDuration;
    }
    
    protected override void BoosterEffect(Cube cubeTrigger)
    {
        cubeTrigger.FreezeCube(_freezeDuration);
        base.BoosterEffect(cubeTrigger);
    }
}
