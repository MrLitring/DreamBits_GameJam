using UnityEngine;
using UnityEngine.UIElements;

public class SampleSxcript : MonoBehaviour
{
    public UIDocument document;
    private VisualElement root;

    private void Start()
    {
        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;

        VisualElement element = root.Q<VisualElement>("square");
    }




}
