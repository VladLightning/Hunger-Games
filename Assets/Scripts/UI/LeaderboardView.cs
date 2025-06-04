using System.Linq;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<Color, string> _colorNames;
    private readonly string _valueColor = ColorUtility.ToHtmlStringRGB(Color.black);
    
    public void UpdateLeaderboardDisplay(TMP_Text display, Color element, int boostersPickedUp)
    {
        string hexColor = ColorUtility.ToHtmlStringRGB(element);
        string finalText = $"<color=#{hexColor}>{GetColorName(element)}</color> : <color=#{_valueColor}>{boostersPickedUp}</color>";
        
        display.text = finalText;
    }

    private string GetColorName(Color color)
    {
        foreach (var kvp in _colorNames.Where(kvp => Mathf.Approximately(kvp.Key.r, color.r) && 
                                                     Mathf.Approximately(kvp.Key.g, color.g) && 
                                                     Mathf.Approximately(kvp.Key.b, color.b) && 
                                                     Mathf.Approximately(kvp.Key.a, color.a)))
        {
            return kvp.Value;
        }

        return "";
    }
}
