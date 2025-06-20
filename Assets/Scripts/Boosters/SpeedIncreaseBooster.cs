

public class SpeedIncreaseBooster : Booster
{
    private float _speedIncreaseCoefficient;

    protected override void Initialize()
    {
        base.Initialize();
        
        var speedIncreaseBoosterData = _boosterData as SpeedIncreaseBoosterData;
        
        _speedIncreaseCoefficient = speedIncreaseBoosterData.SpeedIncreaseCoefficient;
    }
    
    protected override void BoosterEffect(Cube cubeTrigger)
    {
        cubeTrigger.ChangeCubeSpeed(_speedIncreaseCoefficient);
        base.BoosterEffect(cubeTrigger);
    }
}
