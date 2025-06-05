using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CubeNamesData", menuName = "Environment/CubeNames")]
public class CubeNamesData : ScriptableObject
{
    [SerializeField] private List<string> _cubeNames;
    public List<string> CubeNames => _cubeNames;
}
