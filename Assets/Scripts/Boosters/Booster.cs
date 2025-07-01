using System;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public static event Action<GameObject, Color, string, int> OnBoosterPickedUp;
    
    [SerializeField] protected BoosterData _boosterData;
    
    protected int _score;

    protected bool _isPickedUp;
    public bool IsPickedUp => _isPickedUp;

    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        _score = _boosterData.Score;
    }
    
    public virtual void ApplyBooster(Cube cubeTrigger, Color cubeColor, string cubeName, ref int cubeScore)
    {
        cubeScore += _score;
        OnBoosterPickedUp?.Invoke(cubeTrigger.gameObject, cubeColor, cubeName, cubeScore);
        _isPickedUp = true;
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
