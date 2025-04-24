using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI.MessageBox;

public class UIChoosePlayer : MonoBehaviour
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

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        labels = new List<Label>();
        inputHandlers = new List<PlayerInputHandler>();


        CreateUI();
    }

    void CreateUI()
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        root.style.flexDirection = FlexDirection.Column;
        root.style.justifyContent = Justify.FlexStart;
        root.style.alignItems = Align.Center;
        Label title = new Label();
        title.text = "Нажмите Enter, чтобы продолжить";
        title.style.position = Position.Relative;
        title.style.fontSize = Length.Percent(30);
        
        title.style.color = UnityEngine.Color.white;
        title.style.unityFontStyleAndWeight = UnityEngine.FontStyle.Bold;
        
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

        root.Add(title);
        root.Add(sliderInt);




        foreach (var p in players)
        {
            Label label = new Label();
            label.text = "Нажмите Вниз для смены цвета";
            label.style.position = Position.Absolute;
            Vector2 dot = mainCamera.WorldToScreenPoint(p.transform.position);

            label.style.fontSize = 25;
            //p.GetComponent<PlayerData>().SphereColor
            ChangeLabelColor(label, p.GetComponent<PlayerData>().SphereColor);
            label.style.borderLeftWidth = 10f;
            //label.style.left = dot.x - 250;
            //label.style.top = dot.y - 250;
            if (p.transform.position.x > 0)
            {
                label.style.right = Length.Percent(10);
                label.style.top = Length.Percent(20);
            }
            else
            {
                label.style.left = Length.Percent(10);
                label.style.top = Length.Percent(20);
            }
            
            root.Add(label);
            labels.Add(label);
            inputHandlers.Add(p.GetComponent<PlayerInputHandler>());
        }
    }

    void ChangeSliderValue(ChangeEvent<int> evt)
    {
        sliderInt.label = "Выберите количество очков для победы: " + sliderInt.value;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameManager.ScoresPlayer1 = 0;
            gameManager.ScoresPlayer2 = 0;
            gameManager.ScoresToWin = sliderInt.value;
            SceneManager.LoadScene(Random.Range(2,7));
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        for (int i = 0; i < inputHandlers.Count; i++)
        {
            float y = inputHandlers[i].GetMove().y;
            /*if (y > 0)
            {
                PlayerData pd = players[i].GetComponent<PlayerData>();
                int indCol = colorsOfSpheres.colors.IndexOf(pd.SphereColor);
                if (indCol != colorsOfSpheres.colors.Count - 1)
                {
                    pd.ChangeColor(colorsOfSpheres.colors[indCol + 1]);
                    ChangeLabelColor(labels[i], colorsOfSpheres.colors[indCol + 1]);
                }
                else
                {
                    pd.ChangeColor(colorsOfSpheres.colors[0]);
                    ChangeLabelColor(labels[i], colorsOfSpheres.colors[0]);
                }

            }*/
            if (y < 0)
            {
                PlayerData pd = players[i].GetComponent<PlayerData>();
                int indCol = colorsOfSpheres.colors.IndexOf(pd.SphereColor);
                if (indCol != 0)
                {
                    pd.ChangeColor(colorsOfSpheres.colors[indCol - 1]);
                    ChangeLabelColor(labels[i], colorsOfSpheres.colors[indCol - 1]);
                }
                else
                {
                    pd.ChangeColor(colorsOfSpheres.colors[colorsOfSpheres.colors.Count - 1]);
                    ChangeLabelColor(labels[i], colorsOfSpheres.colors[colorsOfSpheres.colors.Count - 1]);
                }
            }
        }
    }
    void ChangeLabelColor(Label label, UnityEngine.Color color)
    {
        label.style.color = color;
        
        label.style.borderLeftColor = color;
        //label.style.borderLeftColor = new UnityEngine.Color(255 - color.r * 255, 255 - color.g * 255, 255 - color.b * 255, 1);
    }
}
