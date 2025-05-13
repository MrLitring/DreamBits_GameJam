using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIChoosePlayerLocal : MonoBehaviour
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

    int countOfScores;


    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        labels = new List<Label>();
        inputHandlers = new List<PlayerInputHandler>();
        CreateUI();
        countOfScores = sliderInt.value;
    }

    
    void CreateUI()
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        root.style.flexDirection = FlexDirection.Column;
        root.style.justifyContent = Justify.FlexStart;
        root.style.alignItems = Align.Center;
        
        Label title = new Label();
        title.text = "Нажмите Enter, чтобы продолжить\nНажмите Space/ЛюбуюСтрелку/X(Gamepad) чтобы присоединиться";
        title.style.position = Position.Relative;
        title.style.fontSize = Length.Percent(30);
        title.style.color = UnityEngine.Color.white;
        title.style.unityFontStyleAndWeight = UnityEngine.FontStyle.Bold;
        root.Add(title);
        


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

    void ChangeSliderValue(ChangeEvent<int> evt)
    {
        countOfScores = sliderInt.value;
        sliderInt.label = "Выберите количество очков для победы: " + sliderInt.value;
    }

   


    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameManager.ScoresPlayers = new Dictionary<int, int>();
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            
            if(players.Length > 1) { 
                for (int i = 0; i < players.Length; i++)
                {
                    gameManager.ScoresPlayers.Add(players[i].GetComponent<PlayerControllerLocal>().Id, 0);
                }
                gameManager.ScoresToWin = sliderInt.value;
                SceneManager.LoadScene("Level" + Random.Range(1,8));
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (GameObject obj in allObjects)
            {
                if (obj.CompareTag("Player"))
                {
                    Destroy(obj);
                }
            }
            SceneManager.LoadScene(0);
        }
    }
}
