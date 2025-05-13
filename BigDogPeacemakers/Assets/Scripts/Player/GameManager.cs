using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameManager", menuName = "Scriptable Objects/GameManager")]
public class GameManager : ScriptableObject
{
    public Dictionary<int, int> ScoresPlayers;
    public int ScoresToWin;
}
