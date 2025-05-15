using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    public bool OnOff;
    List<GameObject> players;
    void Start()
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

        foreach (GameObject p in players)
        {
            p.GetComponent<PlayerControllerLocal>().isActiveMovement = OnOff;
        }
    }
    public void SwitchState(bool state)
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        foreach (GameObject p in players)
        {
            if (p != null) p.GetComponent<PlayerControllerLocal>().isActiveMovement = state;
        }
    }
    
}
