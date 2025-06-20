using System;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public static event Action<GameObject, Color, string, int> OnBoosterPickedUp;
    
    [SerializeField] protected BoosterData _boosterData;
    
    protected int _score;

    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        _score = _boosterData.Score;
    }
    
    public void ApplyBooster(Cube cubeTrigger, Color cubeColor, string cubeName, ref int cubeScore)
    {
        cubeScore += _score;
        OnBoosterPickedUp?.Invoke(cubeTrigger.gameObject, cubeColor, cubeName, cubeScore);
        BoosterEffect(cubeTrigger);
    }

    protected virtual void BoosterEffect(Cube cubeTrigger)
    {
        Dispose();
    }

    private void Dispose()
    {
        Destroy(gameObject);
    }
}
