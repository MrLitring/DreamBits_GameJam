using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class MainMenuUI : MonoBehaviour
{
    UIDocument uiDocument;
    VisualElement root;
    Button buttonCreateLocalGame;
    Button buttonCreateLobby;
    Button buttonConnectToLobby;
    Button buttonSettings;
    Button buttonExit;
    TextField inputJoinCode;
    Button buttonJoin;
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
        buttonCreateLocalGame = root.Q<Button>("button_local_game");
        buttonCreateLobby = root.Q<Button>("button_create_lobby");
        buttonConnectToLobby = root.Q<Button>("button_connect_to_lobby");
        buttonSettings = root.Q<Button>("button_settings");
        buttonExit = root.Q<Button>("button_exit");

        buttonCreateLobby.SetEnabled(false);
        buttonConnectToLobby.SetEnabled(false);
        buttonSettings.SetEnabled(false);



        buttonCreateLocalGame.clicked += CreateLocalGame;
        buttonCreateLobby.clicked += CreateLobby;
        buttonConnectToLobby.clicked += ConnectToLobby;
        buttonSettings.clicked += OpenSettings;
        buttonExit.clicked += ExitGame;


    }
    
    void CreateLocalGame()
    {
        SceneManager.LoadScene("ChoosePlayer");
    }

    void CreateLobby()
    {
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
        
    }

    void ConnectToLobby()
    {
        VisualElement menu = root.Q<VisualElement>("Menu");
        menu.Remove(buttonConnectToLobby);

        VisualElement containerJoinCode = new VisualElement();
        containerJoinCode.style.flexDirection = FlexDirection.Column;


        inputJoinCode = new TextField();

        buttonJoin = new Button();
        buttonJoin.text = "Подключиться";
        buttonJoin.AddToClassList("buttons_main_menu");
        buttonJoin.clicked += ConnectAttempt;

        containerJoinCode.Add(inputJoinCode);
        containerJoinCode.Add(buttonJoin);


        menu.Insert(1, containerJoinCode);



    }

    void ConnectAttempt()
    {
        NetworkManager.Singleton.NetworkConfig.ConnectionApproval = true;
        byte[] payload = System.Text.Encoding.UTF8.GetBytes(inputJoinCode.text);

        Debug.Log("Попытка подключения");
        NetworkManager.Singleton.NetworkConfig.ConnectionData = payload;
        NetworkManager.Singleton.StartClient();
    }



    void OpenSettings()
    {
        // Не реализовано
    }


    void ExitGame()
    {
        Application.Quit();
    }
}
