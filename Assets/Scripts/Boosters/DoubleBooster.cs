
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoubleBooster : Booster
{
    private readonly float _boosterRange = 100f;
    private int _noExtraBoosterFoundScore;

    protected override void Initialize()
    {
        base.Initialize();
        
        var doubleBoosterData = _boosterData as DoubleBoosterData;
        
        _noExtraBoosterFoundScore = doubleBoosterData.NoExtraBoosterFoundScore;
    }

    public override void ApplyBooster(Cube cubeTrigger, Color cubeColor, string cubeName, ref int cubeScore)
    {
        GetExtraBooster(cubeTrigger, cubeColor, cubeName, ref cubeScore);
        base.ApplyBooster(cubeTrigger, cubeColor, cubeName, ref cubeScore);
    }

    private void GetExtraBooster(Cube cubeTrigger, Color cubeColor, string cubeName, ref int cubeScore)
    {
        var boostersOnField = Physics.OverlapSphere(transform.position, _boosterRange, LayerMask.GetMask("Booster"))
            .ToList();

        if (boostersOnField.Count <= 1)
        {
            _score = _noExtraBoosterFoundScore;
            return;
        }
        
        var possibleRandomBoosters = new List<Booster>();
        
        foreach (var boosterCollider in boostersOnField)
        {
            var booster = boosterCollider.GetComponent<Booster>();
            if (booster is not DoubleBooster)
            {
                possibleRandomBoosters.Add(booster);
            }
        }

        if (possibleRandomBoosters.Count == 0)
        {
            _score = _noExtraBoosterFoundScore;
            return;
        }
        
        var randomBooster = possibleRandomBoosters[Random.Range(0, possibleRandomBoosters.Count)].GetComponent<Booster>(); 
        randomBooster.ApplyBooster(cubeTrigger, cubeColor, cubeName, ref cubeScore);
    }
}
