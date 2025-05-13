using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;


public class GameControllerLocal : MonoBehaviour
{
    public GameManager manager;
    public GameObject[] playersSpawnPoint;
    public GameObject visual;
    List<GameObject> players = new List<GameObject>();
    bool stopper = false;
    bool winGame = false;
    PlayerSwitcher playerSwitcher;

    UIDocument uiDocument;
    VisualElement root;
    string name1;
    string name2;


    int activePlayers;
    void Start()
    {
        playerSwitcher = gameObject.GetComponent<PlayerSwitcher>();
        uiDocument = gameObject.GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
        root.style.justifyContent = Justify.FlexStart;
        root.style.alignItems = Align.Center;
        MovePlayersOnSpawnPoint();
    }




    void MovePlayersOnSpawnPoint()
    {
        GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include,FindObjectsSortMode.None);
        players = new List<GameObject>();


        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Player"))  
            {
                players.Add(obj);
                GameObject container = obj.transform.Find("VisContainer").gameObject;
                Animator anim = container.GetComponentInChildren<Animator>();
                Destroy(anim.gameObject);
                GameObject vis = Instantiate(visual, Vector3.zero, Quaternion.identity, container.transform);
                vis.SetActive(false);
                vis.transform.localPosition = Vector3.zero;
                vis.transform.localRotation = Quaternion.identity;
                vis.SetActive(true);
                obj.SetActive(true);

                obj.GetComponent<PlayerControllerLocal>().UpdateAnimator();
            }
            if (obj.CompareTag("InnerObj"))
            {
                obj.SetActive(true);
            }
        }


        for (int i = 0; i < players.Count; i++)
        {
            players[i].SetActive(true);
            Transform[] innerObjects = players[i].GetComponentsInChildren<Transform>();
            foreach (Transform obj in innerObjects)
            {
                obj.gameObject.SetActive(true);
            }
            Animator anim = players[i].GetComponentInChildren<Animator>();
            anim.Rebind();
            anim.Update(0f);
            Rigidbody2D rb = players[i].GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero; 
            rb.angularVelocity = 0;      
            rb.Sleep();
            rb.WakeUp();
            players[i].GetComponent<PlayerState>().HealHealth(100);
            players[i].transform.position = playersSpawnPoint[i].transform.position;
        }
        EnableControl();
        print(" - " + players.Count);
        activePlayers = players.Count;

        
    }


    void Update()
    {
        
        
        if (stopper)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                NextScene();
            }
        }
        
    }

    public void ReculcScores()
    {
        /*
        print("Количество игроков" + players.Count);
        activePlayers = players.Count;
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].activeInHierarchy && !stopper)
            {
                activePlayers--;
                for (int j = 0; j < manager.ScoresPlayers.Count; j++)
                {
                    if (i == j)
                    {
                        manager.ScoresPlayers[j] -= 1;
                    }
                }
                print("Active Players - " + activePlayers);
                
            }
        }*/
        activePlayers = players.Count;
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].activeInHierarchy && !stopper)
            {
                activePlayers--;
            }
        }
        if (activePlayers <= 1)
        {
            DisableControl();
            
            foreach(var key in manager.ScoresPlayers.Keys.ToList())
            {
                manager.ScoresPlayers[key] += 1;
            }

            
            WinOrContinue();
            stopper = true;
        }
    }


    public void DecreaseScore(int id)
    {
        manager.ScoresPlayers[id] -= 1;
        print($"Игрок с ID {id} потерял 1 очко. Текущее количество {manager.ScoresPlayers[id]}");
        ReculcScores();
    }

    void DisableControl()
    {
        playerSwitcher.SwitchState(false);
    }

    void EnableControl()
    {
        playerSwitcher.SwitchState(true);
    }

    void WinOrContinue()
    {
        for (int i = 0; i < manager.ScoresPlayers.Count; i++)
        {
            if (manager.ScoresPlayers[i] >= manager.ScoresToWin)
            {
                winGame = true;
                ShowUI();
                return;
            }
        }
        ShowUI();
    }

    void NextScene()
    {
        if (winGame)
        {
            SceneManager.LoadScene("ChoosePlayer");
        }
        else
        {
            GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (GameObject obj in allObjects)
            {
                if (obj.CompareTag("Bullet"))
                {
                    Destroy(obj);
                }
            }
            SceneManager.LoadScene("Level" + Random.Range(1, 8));
        }


    }

    void ShowUI()
    {
        
        if (winGame)
        {

            string winner = "";
            for (int i = 0; i < manager.ScoresPlayers.Count; i++)
            {
                if (manager.ScoresPlayers[i] >= manager.ScoresToWin)
                {
                    winner = "Player" + players[i].GetComponent<PlayerControllerLocal>().Id;
                    break;
                }

            }

            Label labelResults = new Label();
            
            labelResults.text = $"{winner} is WINNER";
            labelResults.style.fontSize = 60;
            labelResults.style.color = UnityEngine.Color.white;
            root.Add(labelResults);

            GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (GameObject obj in allObjects)
            {
                if (obj.CompareTag("Player") || obj.CompareTag("Bullet"))
                {
                    Destroy(obj);
                }
            }

        }
        else
        {
            VisualElement container = new VisualElement();
            container.style.flexDirection = FlexDirection.Column;
            for (int i = 0; i < manager.ScoresPlayers.Count; i++)
            {
                Label labelResults = new Label();
                labelResults.text = $"{"Player" + players[i].GetComponent<PlayerControllerLocal>().Id} - {manager.ScoresPlayers[players[i].GetComponent<PlayerControllerLocal>().Id]}";
                labelResults.style.fontSize = 40;
                labelResults.style.color = UnityEngine.Color.white;
                container.Add(labelResults);
            }
            root.Add(container);
        }
        
        Label labelTip = new Label();
        labelTip.text = $"Нажмите Enter для продолжения";
        labelTip.style.fontSize = 40;
        labelTip.style.color = UnityEngine.Color.white;
        root.Add(labelTip);
        
    }
}
