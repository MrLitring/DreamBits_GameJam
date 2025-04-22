using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class MainMenuUI : MonoBehaviour
{
    UIDocument uiDocument;
    VisualElement root;

    Button buttonPlay;
    Button buttonSettings;
    Button buttonExit;


    VisualElement visualElement;
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
        buttonPlay = root.Q<Button>("button_play");
        buttonSettings = root.Q<Button>("button_settings");
        buttonExit = root.Q<Button>("button_exit");
        visualElement = root.Q<VisualElement>("curtain");

        buttonPlay.clicked += LoadPlay;
        buttonSettings.clicked += OpenSettings;
        buttonExit.clicked += ExitGame;


        visualElement.visible = false;
    }
    float time = 0;

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time <= 0)
        {
            visualElement.visible = !visualElement.visible;
            time = 5f;
        }
    }



    void LoadPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
