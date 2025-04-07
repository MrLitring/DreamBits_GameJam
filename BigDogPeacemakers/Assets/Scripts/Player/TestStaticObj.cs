using UnityEngine;
using UnityEngine.UIElements;

public class TestStaticObj : MonoBehaviour
{
    int Health;
    UIDocument uiDocument;
    VisualElement root;
    ProgressBar progressBar;
    SpriteRenderer spriteRenderer;
    Color base_color;
    public Camera mainCamera;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        base_color = spriteRenderer.color;
        Health = 5;
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
        progressBar = root.Q<ProgressBar>("ProgressBar");
        progressBar.highValue = Health;
        progressBar.value = Health;
        root.style.width = 50;
        root.style.width = root.style.width.value.value - 10;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        progressBar.value = Health;
        spriteRenderer.color = Color.red;
        WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(2);
        waitForSecondsRealtime.Reset();
        spriteRenderer.color = base_color;
    }

    void Update()
    {
        if (Health < 1)
        {
            gameObject.SetActive(false);
        }


        Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);
        
        root.style.left = screenPos.x - root.layout.width / 2 - 135;
        root.style.top = Screen.height - screenPos.y - 65;
    }
}
