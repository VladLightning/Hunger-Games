using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LeaderboardPresenter : MonoBehaviour
{
    [SerializeField] private Transform _leaderboardDisplay;
    [SerializeField] private TMP_Text _leaderboardTextPrefab;
    
    private LeaderboardView _leaderboardView;
    private Dictionary<string, LeaderboardData> _scores = new Dictionary<string, LeaderboardData>();
    
    private void OnEnable()
    {
        Cube.OnBoosterPickedUp += UpdateElementInLeaderboard;
        CubeSpawner.OnStartSpawn += SpawnDisplays;
    }

    private void OnDisable()
    {
        Cube.OnBoosterPickedUp -= UpdateElementInLeaderboard;
        CubeSpawner.OnStartSpawn -= SpawnDisplays;
    }
    
    private void UpdateElementInLeaderboard(Color color, string cubeName, int value)
    {
        _scores[cubeName].Score = value;
        
        _scores = _scores.OrderByDescending(kvp => kvp.Value.Score).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        int index = 0;
        foreach (var score in _scores)
        {
            _leaderboardView.UpdateLeaderboardDisplay(score.Value.Color, score.Key, score.Value.Score, index);
            index++;
        }
    }

    public void AddLeaderboardElement(Color color, string cubeName)
    {
        _scores.Add(cubeName, new LeaderboardData(color, 0));
        
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

    private class LeaderboardData
    {
        private Color _color;
        public Color Color => _color;
        
        private int _score;
        public int Score { get => _score; set => _score = value; }

        public LeaderboardData(Color color, int score)
        {
            _color = color;
            _score = score;
        }
    }
}
