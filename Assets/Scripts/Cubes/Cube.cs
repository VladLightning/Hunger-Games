using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Cube : MonoBehaviour
{
    public static event Action<GameObject> OnDestroyCube;
    
    private readonly int _radius = 100;
    [SerializeField] private LayerMask _layerMask;
    
    private NavMeshAgent _agent;
    private NavMeshPath _path;
    private Transform _currentTarget;

    private Color _ownColor;
    private int _boostersPickedUpScore;

    private float _constantSpeed;
    private float _temporarySpeed;
    
    private string _cubeName;

    private void OnEnable()
    {
        RoundSystem.OnRoundStart += StartMoving;
    }

    private void OnDisable()
    {
        RoundSystem.OnRoundStart -= StartMoving;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Booster"))
        {
            other.GetComponent<Booster>().ApplyBooster(this, _ownColor, _cubeName, ref _boostersPickedUpScore);
        }
    }

    public void Initialize(Material material,string cubeName, int number)
    {
        _cubeName = cubeName;
        _ownColor = material.color;
        
        GetComponentInChildren<TMP_Text>().text = number.ToString();
        GetComponent<Renderer>().material = material;
    }
    
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _path = new NavMeshPath();
        _constantSpeed = _agent.speed;
    }

    private Transform FindTarget()
    {
        var colliders = Physics.OverlapSphere(transform.position, _radius, _layerMask);
        float shortestDistance = Mathf.Infinity;
        Transform closestTarget = null;
        
        foreach (var collider in colliders)
        {
            if (NavMesh.CalculatePath(transform.position, collider.transform.position, NavMesh.AllAreas, _path))
            {
                float pathLength = GetPathLength(_path);
                if (shortestDistance > pathLength)
                { 
                    shortestDistance = pathLength;
                    closestTarget = collider.transform;
                }
            }
        }
        
        return closestTarget;
    }

    private float GetPathLength(NavMeshPath path)
    {
        float length = 0;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            length += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }
        return length;
    }
    
    private void StartMoving()
    {
        StartCoroutine(Move());
        ResetTemporaryCubeSpeed();
    }

    private IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            
            if (_currentTarget != null)
            {
                continue;
            }
            //Две задержки для того, чтобы raycast не смог сработать раньше уничтожения бустера
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            
            _currentTarget = FindTarget();
            
            if (_currentTarget == null)
            {
                _agent.isStopped = true;
                yield break;
            }
            
            _agent.SetDestination(_currentTarget.position);
        }
    }

    public void ChangeCubeSpeed(float speedCoefficient)
    {
        _agent.speed *= speedCoefficient;
        _constantSpeed = _agent.speed;
    }

    public void SetTemporaryCubeSpeed(float speedCoefficient)
    {
        _temporarySpeed = _agent.speed * speedCoefficient;
        _agent.speed = _temporarySpeed;
    }

    private void ResetTemporaryCubeSpeed()
    {
        _temporarySpeed = 0;
        _agent.speed = _constantSpeed;
    }

    public void SlowDownCube(float slowDownDuration, float coefficient = 0)
    {
        StartCoroutine(ExecuteSlowDownCube(slowDownDuration, coefficient));
    }

    private IEnumerator ExecuteSlowDownCube(float slowDownDuration, float coefficient = 0)
    {
        ApplySlowDownCoefficient(coefficient, true);
        
        yield return new WaitForSeconds(slowDownDuration);
        
        ApplySlowDownCoefficient(1/coefficient, false);
    }

    private void ApplySlowDownCoefficient(float coefficient, bool isStopped)
    {
        if (coefficient == 0)
        {
            _agent.isStopped = isStopped;
        }
        else
        {
            _agent.speed *= coefficient;
        }
    }

    public void DisableCube()
    {
        OnDestroyCube?.Invoke(gameObject);
        gameObject.SetActive(false);
    }
}
