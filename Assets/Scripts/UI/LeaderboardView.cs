using TMPro;
using UnityEngine;

public class LeaderboardView
{
    
    private readonly string _valueColor = ColorUtility.ToHtmlStringRGB(Color.black);
    
    private TMP_Text[] _texts;

    public LeaderboardView(TMP_Text[] texts)
    {
        _texts = texts;
    }
    
    public void UpdateLeaderboardDisplay(Color element, string cubeName, int boostersPickedUp, int index)
    {
        string hexColor = ColorUtility.ToHtmlStringRGB(element);
        string finalText = $"<color=#{hexColor}>{cubeName}</color> : <color=#{_valueColor}>{boostersPickedUp}</color>";
        
        _texts[index].text = finalText;
    }
}
