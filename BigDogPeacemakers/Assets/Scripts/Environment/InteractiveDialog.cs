using UnityEngine;
using UnityEngine.UIElements;

public class InteractiveDialog : MonoBehaviour
{
    ActiveInteraction activeInteraction;

    UIDocument uIDocument;
    VisualElement root;
    VisualElement label;

    float timerInter;
    float cooldownInter;
    bool isActive;

    // Временно тест
    SpriteRenderer spriteRenderer;

    private void Start()
    {

        isActive = false;

        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;
        label = root.Q<VisualElement>("container");
        label.style.position = Position.Absolute;
        timerInter = 0;
        cooldownInter = 1;
        label.visible = false;

        activeInteraction = GetComponent<ActiveInteraction>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

    }

    private void Update()
    {
        UpdateState();
        if (timerInter > 0) timerInter -= Time.deltaTime;
        DoInteract();
    }
    
    private void UpdateState()
    {
        isActive = activeInteraction.IsActive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.enabled = true;
        label.visible = true;
    }/*
    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.enabled= false;
        label.visible = false;
    }*/
    public void DoInteract()
    {
        if (timerInter <= 0 && isActive)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            label.visible = !label.visible;
            print("Взаимодействие");
            timerInter = cooldownInter;
        }
        
    }
}
