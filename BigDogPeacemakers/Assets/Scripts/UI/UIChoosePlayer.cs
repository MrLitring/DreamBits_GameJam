using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIChoosePlayer : MonoBehaviour
{
    public ColorsOfSpheres colorsOfSpheres;
    UIDocument uiDocument;
    VisualElement root;
    Camera mainCamera;
    List<GameObject> players;
    List<PlayerInputHandler> inputHandlers;
    List<Label> labels;
   

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
        root.style.flexDirection = FlexDirection.Row;
        root.style.justifyContent = Justify.Center;
        Label title = new Label();
        title.text = "Нажмите Enter, чтобы продолжить";
        title.style.position = Position.Relative;
        title.style.fontSize = Length.Percent(30);
        
        title.style.color = UnityEngine.Color.white;
        title.style.unityFontStyleAndWeight = UnityEngine.FontStyle.Bold;
        

        root.Add(title);

        foreach (var p in players)
        {
            Label label = new Label();
            label.text = "Нажмите Вправо/Влево для смены цвета";
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        for (int i = 0; i < inputHandlers.Count; i++)
        {
            float x = inputHandlers[i].GetMove().x;
            if (x > 0)
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

            }
            else if (x < 0)
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
