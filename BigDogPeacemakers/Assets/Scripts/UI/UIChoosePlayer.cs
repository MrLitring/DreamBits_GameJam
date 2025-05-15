using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class UIChoosePlayer : NetworkBehaviour
{
    public ColorsOfSpheres colorsOfSpheres;
    public GameManager gameManager;
    UIDocument uiDocument;
    VisualElement root;
    Camera mainCamera;
    List<GameObject> players;
    List<PlayerInputHandler> inputHandlers;
    List<Label> labels;
    SliderInt sliderInt;

    NetworkVariable<int> countOfScores = new NetworkVariable<int>();


    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        labels = new List<Label>();
        inputHandlers = new List<PlayerInputHandler>();
        CreateUI();
    }

    public override void OnNetworkSpawn()
    {
        countOfScores.OnValueChanged += OnScoreCountChanged;
        if (IsServer)
        {
            countOfScores.Value = sliderInt.value;
        }
        print("OnNetworkSpawn");
    }
    void CreateUI()
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        root.style.flexDirection = FlexDirection.Column;
        root.style.justifyContent = Justify.FlexStart;
        root.style.alignItems = Align.Center;
        if (IsServer)
        {
            Label title = new Label();
            title.text = "Нажмите Enter, чтобы продолжить";
            title.style.position = Position.Relative;
            title.style.fontSize = Length.Percent(30);
            title.style.color = UnityEngine.Color.white;
            title.style.unityFontStyleAndWeight = UnityEngine.FontStyle.Bold;
            root.Add(title);
        }


        sliderInt = new SliderInt();

        sliderInt.value = 5;
        sliderInt.lowValue = 1;
        sliderInt.highValue = 10;
        sliderInt.label = "Выберите количество очков для победы: " + sliderInt.value;
        sliderInt.style.width = Length.Percent(30);
        sliderInt.style.position = Position.Relative;
        sliderInt.style.color = UnityEngine.Color.white;
        sliderInt.style.unityFontStyleAndWeight = UnityEngine.FontStyle.Bold;
        sliderInt.RegisterValueChangedCallback(ChangeSliderValue);

        

        root.Add(sliderInt);




        
    }

    void OnScoreCountChanged(int prev, int curr)
    {
        sliderInt.value = curr;
        sliderInt.label = "Выберите количество очков для победы: " + sliderInt.value;
    }
    
    void ChangeSliderValue(ChangeEvent<int> evt)
    {
        if (IsServer)
        {
            countOfScores.Value = sliderInt.value;
            Debug.Log("[Server]Значение очков изменено");
        }
        else
        {
            ChangeCountValueServerRpc(sliderInt.value);
            Debug.Log("[Client]Значение очков изменено");
        }
        
    }
    [ServerRpc(RequireOwnership =false)]
    void ChangeCountValueServerRpc(int count)
    {
        countOfScores.Value = count;
    }



    private void Update()
    {
        if (IsServer)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                gameManager.ScoresPlayers = new Dictionary<int, int>();
                for (int i = 0; i < NetworkManager.ConnectedClientsIds.Count; i++)
                {
                    //gameManager.ScoresPlayers.Add(NetworkManager.ConnectedClientsIds[i], 0);
                }
                gameManager.ScoresToWin = sliderInt.value;
                //NetworkManager.SceneManager.LoadScene("Level" + Random.Range(1, 7), LoadSceneMode.Single);
                NetworkManager.SceneManager.LoadScene("Level" + 5, LoadSceneMode.Single);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            NetworkManager.Singleton.Shutdown();
            SceneManager.LoadScene(0);
        }
    }
    
}
