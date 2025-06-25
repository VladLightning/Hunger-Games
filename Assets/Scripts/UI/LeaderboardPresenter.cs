using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LeaderboardPresenter : MonoBehaviour
{
    [SerializeField] private Transform _leaderboardDisplay;
    [SerializeField] private TMP_Text _leaderboardTextPrefab;
    
    private LeaderboardView _leaderboardView;
    private Dictionary<GameObject, LeaderboardData> _scores = new Dictionary<GameObject, LeaderboardData>();
    
    private void OnEnable()
    {
        RoundSystem.OnEliminateLastPlace += EliminateLastPlace;
        Booster.OnBoosterPickedUp += UpdateElementInLeaderboard;

        FreezeFirstOrLastBooster.OnFreezeFirstOrLast += GetLeaderboardPresenter;
    }

    private void OnDisable()
    {
        RoundSystem.OnEliminateLastPlace -= EliminateLastPlace;
        Booster.OnBoosterPickedUp -= UpdateElementInLeaderboard;
        
        FreezeFirstOrLastBooster.OnFreezeFirstOrLast -= GetLeaderboardPresenter;
    }
    
    private void EliminateLastPlace()
    {
        var lastPlaceCube = GetLastPlaceCube();

        lastPlaceCube.GetComponent<Cube>().DisableCube();
    }

    private GameObject GetLastPlaceCube()
    {
        GameObject lastPlaceCube = null;
        
        var keys = _scores.Keys.ToList();

        for (int i = keys.Count - 1; i >= 0; i--)
        {
            if (keys[i].activeInHierarchy)
            {
                lastPlaceCube = keys[i];
                break;
            }
        }

        return lastPlaceCube;
    }

    private void UpdateElementInLeaderboard(GameObject cube, Color color, string cubeName, int value)
    {
        if (!_scores.ContainsKey(cube))
        {
            throw new ArgumentException($"Dictionary {_scores} does not contain {cube}");
        }
        _scores[cube].Score = value;
        
        _scores = _scores.OrderByDescending(kvp => kvp.Value.Score).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        int index = 0;
        foreach (var score in _scores)
        {
            _leaderboardView.UpdateLeaderboardDisplay(score.Value.Color, score.Value.Name, score.Value.Score, index);
            index++;
        }
    }

    public void AddLeaderboardElement(GameObject cube, Color color, string cubeName)
    {
        _scores.Add(cube, new LeaderboardData(color, cubeName,0));
        
        _leaderboardView.UpdateLeaderboardDisplay(color, cubeName, 0, _scores.Count - 1);
    }

    public void SpawnDisplays(int amount)
    {
        var texts = new TMP_Text[amount];
        for (int i = 0; i < amount; i++)
        {
            texts[i] = Instantiate(_leaderboardTextPrefab, _leaderboardDisplay);
        }
        _leaderboardView = new LeaderboardView(texts);
    }

    private LeaderboardPresenter GetLeaderboardPresenter()
    {
        return this;
    }

    public GameObject GetFirstPlace()
    {
        return _scores.First().Key;
    }

    public GameObject GetLastPlace()
    {
        return GetLastPlaceCube();
    }

    private class LeaderboardData
    {
        private Color _color;
        public Color Color => _color;
        
        private string _name;
        public string Name => _name;
        
        private int _score;
        public int Score { get => _score; set => _score = value; }

        public LeaderboardData(Color color, string cubeName, int score)
        {
            _color = color;
            _name = cubeName;
            _score = score;
        }
    }
}
