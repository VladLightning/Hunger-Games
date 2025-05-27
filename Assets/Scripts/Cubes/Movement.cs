using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private readonly int _radius = 100;
    [SerializeField] private LayerMask _layerMask;
    
    private NavMeshAgent _agent;
    private NavMeshPath _path;
    private Transform _currentTarget;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _path = new NavMeshPath();
        _agent.SetDestination(FindTarget().position);
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
}
