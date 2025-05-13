using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
public class ControlTips : MonoBehaviour
{
    UIDocument uiDocument;
    VisualElement root;
    VisualElement tipsContainer;
    
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
        root.style.flexDirection = FlexDirection.Column;
        root.style.justifyContent = Justify.FlexEnd;

        tipsContainer = new VisualElement();
        tipsContainer.style.flexDirection = FlexDirection.Row;
        tipsContainer.style.justifyContent = Justify.SpaceAround;
        

        player1Tip = new VisualElement();
        player1Tip.style.position = Position.Absolute;
        player1Tip.style.left = 0;
        player1Tip.style.bottom = 0;
        player1Tip.style.width = Length.Percent(30);
        player1Tip.style.height = Length.Percent(10);

        Label labelTip1 = new Label();
        labelTip1.text = "Клавиатура WASD:\nПередвижение - W D\nАтака - H\nПрыжок - Space";
        labelTip1.style.color = Color.white;
        labelTip1.style.fontSize = 20;

        


        player2Tip = new VisualElement();
        player2Tip.style.position = Position.Absolute;
        player2Tip.style.right = 0;
        player2Tip.style.bottom = 0;
        player2Tip.style.width = Length.Percent(30);
        player2Tip.style.height = Length.Percent(10);

        Label labelTip2 = new Label();
        labelTip2.text = "Геймпад:\nПередвижение - Левый стик \nАтака - Х\nПрыжок - R1";
        labelTip2.style.color = Color.white;
        labelTip2.style.fontSize = 20;
        //labelTip2.style.width = Length.Percent(30);
        //labelTip2.style.height = Length.Percent(10);



        Label labelTip3 = new Label();
        labelTip3.text = "Клавиатура Стрелки:\nПередвижение - Стрелка влево Стрелка вправо \nАтака - NumPeriod(.)\nПрыжок - Num0";
        labelTip3.style.color = Color.white;
        labelTip3.style.fontSize = 20;



        //root.Add(player1Tip);
        //root.Add(player2Tip);
        //player1Tip.Add(labelTip1);
        //player2Tip.Add(labelTip2);

        //player1Tip.style.visibility = Visibility.Hidden;
        //player2Tip.style.visibility = Visibility.Hidden;

        root.Add(tipsContainer);
        tipsContainer.Add(labelTip1);
        tipsContainer.Add(labelTip2);
        tipsContainer.Add(labelTip3);

        tipsContainer.style.visibility = Visibility.Hidden;


        Label staticTip = new Label();
        staticTip.text = "I - управление\nEsc - выйти в меню";
        staticTip.style.color = Color.white;
        staticTip.style.fontSize = 20;
        staticTip.style.width = Length.Percent(30);
        staticTip.style.height = Length.Percent(10);
        staticTip.style.position = Position.Absolute;
        staticTip.style.left = 10;
        staticTip.style.top = 10;
        root.Add(staticTip);

    }
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            tipsContainer.style.visibility = tipsContainer.style.visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
            //player1Tip.style.visibility = player1Tip.style.visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
            //player2Tip.style.visibility = player2Tip.style.visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
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
