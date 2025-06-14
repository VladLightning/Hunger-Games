using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] private BoosterData _boosterData;
    
    private int _score;

    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        _score = _boosterData.Score;
    }
    
    public virtual int ApplyBooster()
    {
        Destroy(gameObject);
        return _score;
    }
}
