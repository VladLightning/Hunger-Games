
using UnityEngine;

public class GameSpeedScalerInit : MonoBehaviour
{
    [SerializeField] private GameSpeedScalerView _gameSpeedScalerView;
    private GameSpeedScalerPresenter _gameSpeedScalerPresenter;
    
    private void Start()
    {
        var gameSpeedScalerModel = new GameSpeedScalerModel();
        _gameSpeedScalerPresenter = new GameSpeedScalerPresenter(_gameSpeedScalerView, gameSpeedScalerModel);
    }

    private void OnDestroy()
    {
        _gameSpeedScalerPresenter.Dispose();
    }
}
