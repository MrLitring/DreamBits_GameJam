using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "Scriptable Objects/GameManager")]
public class GameManager : ScriptableObject
{
    public int ScoresPlayer1;
    public int ScoresPlayer2;
    public int ScoresToWin;
}
