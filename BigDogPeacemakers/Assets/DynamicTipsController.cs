using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.MessageBox;

public class DynamicTipsController : MonoBehaviour
{
    UIDocument uiDocument;
    VisualElement dynamicTipContainer;
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        VisualElement root = uiDocument.rootVisualElement;

        dynamicTipContainer = new VisualElement();

        dynamicTipContainer.style.position = Position.Absolute;
        dynamicTipContainer.style.right = 10;
        dynamicTipContainer.style.top = 10;
        dynamicTipContainer.style.flexDirection = FlexDirection.Column;

        root.Add(dynamicTipContainer);

    }

    public IEnumerator CreateTip(string tipText)
    {
        Label label = new Label();
        label.text = tipText;
        label.style.fontSize = 30;
        label.style.color = Color.white;
        dynamicTipContainer.Add(label);
        yield return new WaitForSeconds(5);
        dynamicTipContainer.Remove(label);
    }
}
