using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Unity.Netcode;

public class GameController : NetworkBehaviour
{
    public GameManager manager;
    public GameObject[] playersSpawnPoint;
    
    bool stopper = false;
    bool winGame = false;
    PlayerSwitcher playerSwitcher;

    UIDocument uiDocument;
    VisualElement root;
    string name1;
    string name2;
    void Start()
    {
        playerSwitcher = gameObject.GetComponent<PlayerSwitcher>();
        uiDocument = gameObject.GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
        root.style.justifyContent = Justify.FlexStart;
        root.style.alignItems = Align.Center;
        //name1 = player1.name;
        //name2 = player2.name;

    }


    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            MovePlayersOnSpawnPointServerRpc();
        }
        
    }

    [ServerRpc(RequireOwnership = false)]
    void MovePlayersOnSpawnPointServerRpc()
    {
        MovePlayersOnSpawnPointClientRpc();
    }
    
    [ClientRpc]
    void MovePlayersOnSpawnPointClientRpc()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = playersSpawnPoint[i].transform.position;
        }
    }
    void Update()
    {
        /*
        if(player1.IsDestroyed() && !stopper){
            print("Player1 is Died");
            //manager.ScoresPlayer2 += 1;
            DisableControl();
            WinOrContinue();
            stopper = true;
        }
        if (player2.IsDestroyed() && !stopper)
        {
            print("Player2 is Died");
            //manager.ScoresPlayer1 += 1;
            DisableControl();
            WinOrContinue();
            stopper = true;
        }
        if (stopper)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                NextScene();
            }
        }
        */
    }

    void DisableControl()
    {
        playerSwitcher.SwitchState(false);
    }
    
    void WinOrContinue()
    {
        /*
        if (manager.ScoresPlayer1 >= manager.ScoresToWin || manager.ScoresPlayer2 >= manager.ScoresToWin)
        {
            winGame = true;
            ShowUI();
        }
        else
        {
            ShowUI();
        }
        */
    }

    void NextScene()
    {
        if (winGame) {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(Random.Range(2, 7));
        }

        
    }

    void ShowUI()
    {
        /*
        if (winGame)
        {
            Label labelResults = new Label();
            string name = manager.ScoresPlayer1 > manager.ScoresPlayer2 ? name1 : name2;
            labelResults.text = $"{name} is WINNER";
            labelResults.style.fontSize = 60;
            labelResults.style.color = UnityEngine.Color.white;
            root.Add(labelResults);
        }
        else
        {
            Label labelResults = new Label();
            labelResults.text = $"{name1} - {manager.ScoresPlayer1} \n{name2} - {manager.ScoresPlayer2} ";
            labelResults.style.fontSize = 40;
            labelResults.style.color = UnityEngine.Color.white;
            root.Add(labelResults);
        }
        
        Label labelTip = new Label();
        labelTip.text = $"Нажмите Enter для продолжения";
        labelTip.style.fontSize = 40;
        labelTip.style.color = UnityEngine.Color.white;
        root.Add(labelTip);
        */
    }
}
