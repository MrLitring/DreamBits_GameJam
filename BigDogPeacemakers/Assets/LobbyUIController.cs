using UnityEngine;
using Unity.Netcode;
using UnityEngine.UIElements;
using UnityEngine.Splines.ExtrusionShapes;
using UnityEngine.SceneManagement;

public class LobbyUIController : NetworkBehaviour
{

    UIDocument uIDocument;
    VisualElement root;
    VisualElement playerContainer;
    Button buttonReady;
    NetworkVariable<int> countOfReady = new NetworkVariable<int>();
    
    bool isReady = false;

    public LobbyManager manager;

    void Awake()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;
        playerContainer = root.Q<VisualElement>("players_list");
        buttonReady = root.Q<Button>("button_ready");
        manager.playerList.OnListChanged += UpdatePlayerList;
        buttonReady.clicked += Click_ReadyButton;
        countOfReady.OnValueChanged += NextLevelServerRpc;
    }

    private void UpdatePlayerList(NetworkListEvent<ulong> changeEvent)
    {
        playerContainer.Clear();
        for (int i = 0; i < manager.playerList.Count; i++)
        {
            AddPlayer(manager.playerList[i]);
        }
    }


    public void AddPlayer(ulong id)
    {
        VisualElement player = CreatePlayer(id);

        playerContainer.Add(player);
    }

    public void RemovePlayer(ulong id)
    {
        playerContainer.Remove(playerContainer.Q<VisualElement>(id.ToString()));
    }

    VisualElement CreatePlayer(ulong id)
    {
        VisualElement player = new VisualElement();
        player.name = id.ToString();

        VisualElement playerIcon = new VisualElement();
        playerIcon.name = "icon";
        playerIcon.style.width = 15;
        playerIcon.style.height = 15;
        playerIcon.style.borderTopLeftRadius = 15;
        playerIcon.style.borderBottomLeftRadius = 15;
        playerIcon.style.borderTopRightRadius = 15;
        playerIcon.style.borderBottomRightRadius = 15;
        playerIcon.style.backgroundColor = new Color(Random.Range(0, 256), Random.Range(0, 256), Random.Range(0, 256), Random.Range(0, 256));

        Label playerName = new Label();
        playerName.text = "Player with id - " + id;


        player.Add(playerIcon);
        player.Add(playerName);


        return player;
    }

    void Click_ReadyButton()
    {
        if (!isReady)
        {
            isReady = true;
            buttonReady.AddToClassList("button_On");
            buttonReady.RemoveFromClassList("button_Off");
            buttonReady.text = "√Œ“Œ¬";
            ChangeCircleColorServerRpc(NetworkManager.Singleton.LocalClientId.ToString(), Color.red);
            
        }
        else
        {
            isReady = false;
            buttonReady.RemoveFromClassList("button_On");
            buttonReady.AddToClassList("button_Off");
            buttonReady.text = "ÕÂ „ÓÚÓ‚";
            ChangeCircleColorServerRpc(NetworkManager.Singleton.LocalClientId.ToString(), Color.white);
            
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void ChangeCircleColorServerRpc(string nameElement, Color color)
    {
        ChangeCircleColorClientRpc(nameElement, color);
        if (IsServer) { 
            if (color == Color.red)
            {
                countOfReady.Value++;
            }
            else
            {
                countOfReady.Value--;
            }
            print(countOfReady.Value);
        }
    }


    [ClientRpc]
    void ChangeCircleColorClientRpc(string nameElement, Color color)
    {
        var ve = playerContainer.Q<VisualElement>(nameElement);
        var icon = ve.Q<VisualElement>("icon");
        icon.style.backgroundColor = color;   
    }

    [ServerRpc(RequireOwnership = false)]
    void NextLevelServerRpc(int prev, int current)
    {
        if (IsServer && IsOwner)
        {
            if(current == manager.playerList.Count)
            {
                NetworkManager.Singleton.SceneManager.LoadScene("ChoosePlayer", LoadSceneMode.Single);
            }
        }
    }
}
