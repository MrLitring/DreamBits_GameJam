using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;
using System.Collections;


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
        stopper = false;
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
        if (players is null) return;

        if (activePlayers == 1)
        {
            DisableControl();

            foreach (var key in manager.ScoresPlayers.Keys.ToList())
            {
                manager.ScoresPlayers[key] += 1;
            }


            WinOrContinue();
            stopper = true;
        }
    }


    public void DecreaseScore(int id)
    {
        if (stopper == false)
        {
            manager.ScoresPlayers[id] -= 1;
            activePlayers -= 1;
            print($"Игрок с ID {id} потерял 1 очко. Текущее количество {manager.ScoresPlayers[id]}");
            ReculcScores();
        }
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
        if (isMaxScore(manager.ScoresToWin, out _))
            winGame = true;

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

            isMaxScore(manager.ScoresToWin, out int playerIndex);
            string winner = "Player" + players[playerIndex].GetComponent<PlayerControllerLocal>().Id;

            Label labelResults = LabelResult($"{winner} is WINNER", 60);
            root.Add(labelResults);

            ObjDestroy();

        }
        else
        {
            VisualElement container = new VisualElement();
            container.style.flexDirection = FlexDirection.Column;
            for (int i = 0; i < manager.ScoresPlayers.Count; i++)
            {
                Label labelResults = LabelResult($"{"Player" + players[i].GetComponent<PlayerControllerLocal>().Id} - {manager.ScoresPlayers[players[i].GetComponent<PlayerControllerLocal>().Id]}", 40);
                container.Add(labelResults);
            }
            root.Add(container);
        }

        Label labelTip = LabelResult("Нажмите Enter для продолжения");
        root.Add(labelTip);

    }

    private Label LabelResult(string content, int size = 40)
    {
        Label label = new Label();
        label.text = content;
        label.style.color = UnityEngine.Color.white;
        label.style.fontSize = size;

        return label;
    }


    private bool isMaxScore(int value, out int playerIndex)
    {
        for (int i = 0; i < manager.ScoresPlayers.Count; i++)
        {
            if (manager.ScoresPlayers[i] >= manager.ScoresToWin)
            {
                playerIndex = i;
                return true;
            }
        }

        playerIndex = -1;
        return false;
    }

    private void ObjDestroy()
    {
        GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Player") || obj.CompareTag("Bullet"))
            {
                Destroy(obj);
            }
        }
    }


}
