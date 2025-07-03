
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBooster : Booster
{
    private readonly float _boosterRange = 100f;
    private float _navigatedBoosterSpeed;
    
    private NavMeshPath _path;

    protected override void Initialize()
    {
        base.Initialize();
        
        var navigationBoosterData = _boosterData as NavigationBoosterData;

        _navigatedBoosterSpeed = navigationBoosterData.NavigatedBoosterSpeed;
        
        _path = new NavMeshPath();
    }

    protected override void BoosterEffect(Cube cubeTrigger)
    {
        NavigateClosestBooster(cubeTrigger);
        base.BoosterEffect(cubeTrigger);
    }

    private void NavigateClosestBooster(Cube cubeTrigger)
    {
        var boostersOnField = Physics.OverlapSphere(transform.position, _boosterRange, LayerMask.GetMask("Booster"));
        
        if (boostersOnField.Length <= 1)
        {
            return;
        }
        
        var closestBooster = GetClosestBooster(boostersOnField).GetComponent<Booster>();
        
        closestBooster.StartMoving(cubeTrigger.transform, _navigatedBoosterSpeed);
    }

    private Transform GetClosestBooster(Collider[] boostersOnField)
    {
        var possibleClosestBoosters = new List<Booster>();
        
        foreach (var boosterCollider in boostersOnField)
        {
            var booster = boosterCollider.GetComponent<Booster>();
            if (!booster.IsPickedUp)
            {
                possibleClosestBoosters.Add(booster);
            }
        }
        
        if (possibleClosestBoosters.Count == 0)
        { 
            return null;
        }
        
        float shortestDistance = Mathf.Infinity;
        Transform closestBooster = null;
        
        foreach (var booster in possibleClosestBoosters)
        {
            if (NavMesh.CalculatePath(transform.position, booster.transform.position, NavMesh.AllAreas, _path))
            {
                float pathLength = GetPathLength(_path);
                if (shortestDistance > pathLength)
                { 
                    shortestDistance = pathLength;
                    closestBooster = booster.transform;
                }
            }
        }
        
        return closestBooster;
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
