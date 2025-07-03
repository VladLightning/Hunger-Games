using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Booster : MonoBehaviour
{
    public static event Action<GameObject, Color, string, int> OnBoosterPickedUp;

    private readonly float _setDestinationDelay = 0.05f;
    
    [SerializeField] protected BoosterData _boosterData;
    
    protected int _score;

    protected bool _isPickedUp;
    public bool IsPickedUp => _isPickedUp;

    private NavMeshAgent _agent;
    private Transform _currentTarget;
    private Rigidbody _rigidbody;

    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        _score = _boosterData.Score;
    }

    public void StartMoving(Transform target, float speed)
    {
        StartCoroutine(Move(target, speed));
    }

    private IEnumerator Move(Transform target, float speed)
    {
        _currentTarget = target;
        _agent.speed = speed;
        
        _rigidbody.isKinematic = true;
        
        while (true)
        {
            yield return new WaitForFixedUpdate();
            
            if (_currentTarget == null)
            {
                _agent.isStopped = true;
                _rigidbody.isKinematic = false;
                yield break;
            }
            //Задержка для того, чтобы уже уничтоженный бустер не пытался задать себе путь,
            //переменные _agent.enabled и _agent.IsActiveAndEnabled становятся false, от чего происходит ошибка
            yield return new WaitForSeconds(_setDestinationDelay); 
            
            _agent.SetDestination(_currentTarget.position);
        }
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
