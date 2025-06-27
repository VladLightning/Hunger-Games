
using UnityEngine;

public class GameSpeedScalerPresenter
{
    private readonly GameSpeedScalerView _gameSpeedScalerView;
    private readonly GameSpeedScalerModel _gameSpeedScalerModel;

    public GameSpeedScalerPresenter(GameSpeedScalerView gameSpeedScalerView, GameSpeedScalerModel gameSpeedScalerModel)
    {
        _gameSpeedScalerView = gameSpeedScalerView;
        _gameSpeedScalerModel = gameSpeedScalerModel;

        _gameSpeedScalerView.UpdateTimeScaleDisplay(Time.timeScale);

        GameSpeedScalerView.OnChangeTimeScale += ChangeTimeScale;
        GameSpeedScalerModel.OnTimeScaleChanged += UpdateTimeScale;
    }

    public void Unsubscribe()
    {
        GameSpeedScalerView.OnChangeTimeScale -= ChangeTimeScale;
        GameSpeedScalerModel.OnTimeScaleChanged -= UpdateTimeScale;
        _gameSpeedScalerModel.ResetTimeScale();
    }

    private void ChangeTimeScale(float timeScaleChangeFactor)
    {
        _gameSpeedScalerModel.ChangeTimeScale(timeScaleChangeFactor);
    }

    private void UpdateTimeScale(float timeScale)
    {
        _gameSpeedScalerView.UpdateTimeScaleDisplay(timeScale);
    }
}
