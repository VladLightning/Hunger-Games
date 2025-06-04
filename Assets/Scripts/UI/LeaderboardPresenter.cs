using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LeaderboardPresenter : MonoBehaviour
{
    [SerializeField] private LeaderboardView _leaderboardView;
    
    [SerializeField] private Transform _leaderboardDisplay;
    [SerializeField] private TMP_Text _leaderboardTextPrefab;
    
    private Dictionary<Color, int> _leaderboardElements = new Dictionary<Color, int>();
    private Dictionary<Color, TMP_Text> _leaderboardElementsText = new Dictionary<Color, TMP_Text>();

    private void OnEnable()
    {
        Cube.OnJoinLeaderboard += AddElementToLeaderboard;
        Cube.OnBoosterPickedUp += UpdateElementInLeaderboard;
    }

    private void OnDisable()
    {
        Cube.OnJoinLeaderboard -= AddElementToLeaderboard;
        Cube.OnBoosterPickedUp -= UpdateElementInLeaderboard;
    }

    private void AddElementToLeaderboard(Color color, int value)
    {
        _leaderboardElements.Add(color, value);
        
        var text = Instantiate(_leaderboardTextPrefab, _leaderboardDisplay);
        _leaderboardElementsText.Add(color, text);
        
        _leaderboardView.UpdateLeaderboardDisplay(_leaderboardElementsText[color], color, value);
    }

    private void UpdateElementInLeaderboard(Color color, int value)
    {
        _leaderboardElements[color] = value;
        _leaderboardView.UpdateLeaderboardDisplay(_leaderboardElementsText[color], color, value);
        
        OrderLeaderboardPlaces();
    }

    private void OrderLeaderboardPlaces()
    {
        var order = _leaderboardElements.OrderByDescending(kvp => kvp.Value);
        var colors = order.ToDictionary(kvp => kvp.Key, kvp => kvp.Value).Keys.ToList();

        int index = 0;
        foreach (var color in colors)
        {
            _leaderboardElementsText[color].transform.SetSiblingIndex(index);
            index++;
        }
    }
}
