
using UnityEngine;
using UnityEngine.UIElements;
public class ControlTips : MonoBehaviour
{
    UIDocument uiDocument;
    VisualElement root;
    VisualElement player1Tip;
    VisualElement player2Tip;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
        CreateUI();
        
    }
    void CreateUI()
    {


        player1Tip = new VisualElement();
        player1Tip.style.position = Position.Absolute;
        player1Tip.style.left = 0;
        player1Tip.style.bottom = 0;
        player1Tip.style.width = Length.Percent(30);
        player1Tip.style.height = Length.Percent(10);

        Label labelTip1 = new Label();
        labelTip1.text = "Передвижение - W D\nАтака - H\nПрыжок - Space";
        labelTip1.style.color = Color.white;
        labelTip1.style.fontSize = 20;

        


        player2Tip = new VisualElement();
        player2Tip.style.position = Position.Absolute;
        player2Tip.style.right = 0;
        player2Tip.style.bottom = 0;
        player2Tip.style.width = Length.Percent(30);
        player2Tip.style.height = Length.Percent(10);

        Label labelTip2 = new Label();
        labelTip2.text = "Передвижение - Стрелка Влево и стрелка Вправо \nАтака - Enter/Левая кнопка мыши\nПрыжок - Стрелка Вверх";
        labelTip2.style.color = Color.white;
        labelTip2.style.fontSize = 20;
        labelTip2.style.width = 200;
        labelTip2.style.height = 100;


        player2Tip.Add(labelTip2);

        root.Add(player1Tip);
        root.Add(labelTip2);
        player1Tip.Add(labelTip1);

        Label lbl = new Label();
        lbl.text = "aaaaaaaaaaaaaaaaaaaaaaaa";
        lbl.style.color = Color.white;
        lbl.style.fontSize = 20;
        root.Add(lbl);
    }
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            player1Tip.SetEnabled(true);
            player2Tip.SetEnabled(true);
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            player1Tip.SetEnabled(false);
            player2Tip.SetEnabled(false);
        }

    }
}
