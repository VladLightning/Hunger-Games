
public class CurrentRoundSpeedIncreaseBooster : Booster
{
    private float _speedIncreaseCoefficient;

    protected override void Initialize()
    {
        base.Initialize();
        
        var currentRoundSpeedIncreaseBoosterData = _boosterData as CurrentRoundSpeedIncreaseBoosterData;
        
        _speedIncreaseCoefficient = currentRoundSpeedIncreaseBoosterData.SpeedIncreaseCoefficient;
    }
    
    protected override void BoosterEffect(Cube cubeTrigger)
    {
        cubeTrigger.SetTemporaryCubeSpeed(_speedIncreaseCoefficient);
        base.BoosterEffect(cubeTrigger);
    }
}
