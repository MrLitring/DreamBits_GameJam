using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUI : MonoBehaviour
{
    public Vector2 offset;
    UIDocument uiDocument;
    VisualElement container;
    int playerId;
    Camera mainCamera;

    float pixelPerUnit;
    
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerId = GetComponent<PlayerControllerLocal>().Id;
        uiDocument = GetComponent<UIDocument>();
        
        container = new VisualElement();
        container.style.position = Position.Absolute;
        Label nickname = new Label();
        nickname.text = "Player " + playerId;
        nickname.style.color = Color.white;
        container.Add(nickname);
        uiDocument.rootVisualElement.Clear();
        uiDocument.rootVisualElement.Add(container);

        
        Vector2 screenPos = mainCamera.WorldToScreenPoint(transform.position);
        container.style.left = screenPos.x - container.layout.width / 2 + offset.x;
        container.style.top = Screen.height - screenPos.y - offset.y;

        
    }

    private void OnEnable()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerId = GetComponent<PlayerControllerLocal>().Id;
        uiDocument = GetComponent<UIDocument>();

        container = new VisualElement();
        container.style.position = Position.Absolute;
        Label nickname = new Label();
        nickname.text = "Player " + playerId;
        nickname.style.color = Color.white;
        container.Add(nickname);
        uiDocument.rootVisualElement.Clear();
        uiDocument.rootVisualElement.Add(container);


        Vector2 screenPos = mainCamera.WorldToScreenPoint(transform.position);
        container.style.left = screenPos.x - container.layout.width / 2 + offset.x;
        container.style.top = Screen.height - screenPos.y - offset.y;
    }
    // Update is called once per frame
    void Update()
    {
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        Vector2 screenPos = mainCamera.WorldToScreenPoint(transform.position);
        pixelPerUnit = mainCamera.pixelHeight / (2f * mainCamera.orthographicSize);
        print(pixelPerUnit);
        container.style.left = screenPos.x - container.layout.width / 2 + offset.x;
        container.style.top = Screen.height - screenPos.y - offset.y * pixelPerUnit;
    }
}
