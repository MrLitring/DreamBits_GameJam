using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerLocalManager : MonoBehaviour
{
    public int maxPlayers = 4;
    public GameObject player;
    public Vector2 startPos;
    public float distanceBetweenX;

    List<GameObject> gameObjects;
    Dictionary<Controller, int> listControls = new Dictionary<Controller, int>()
    {
        {Controller.Keyboard1, 0},
        {Controller.Keyboard2, 0},
        {Controller.Gamepad1, 0},
        {Controller.Gamepad2, 0},
        {Controller.Gamepad3, 0},
        {Controller.Gamepad4, 0}
    };

    void Start()
    {
        gameObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        
    }

    void Update()
    {
        gameObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        if (gameObjects.Count < maxPlayers)
        {
            
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X)) && listControls[Controller.Keyboard1] == 0)
            {
                CreatePlayer(Controller.Keyboard1);
                listControls[Controller.Keyboard1] = 1;
            }
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && listControls[Controller.Keyboard2] == 0)
            {
                CreatePlayer(Controller.Keyboard2);
                listControls[Controller.Keyboard2] = 1;
            }
            if (Input.GetKeyDown(KeyCode.Joystick1Button1) && listControls[Controller.Gamepad1] == 0)
            {
                CreatePlayer(Controller.Gamepad1);
                listControls[Controller.Gamepad1] = 1;
            }
            if (Input.GetKeyDown(KeyCode.Joystick2Button1) && listControls[Controller.Gamepad2] == 0)
            {
                CreatePlayer(Controller.Gamepad2);
                listControls[Controller.Gamepad2] = 1;
            }
            if (Input.GetKeyDown(KeyCode.Joystick3Button1) && listControls[Controller.Gamepad3] == 0)
            {
                CreatePlayer(Controller.Gamepad3);
                listControls[Controller.Gamepad3] = 1;
            }
            if (Input.GetKeyDown(KeyCode.Joystick4Button1) && listControls[Controller.Gamepad4] == 0)
            {
                CreatePlayer(Controller.Gamepad4);
                listControls[Controller.Gamepad4] = 1;
            }
        }
        

        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            Debug.Log("KeyCode.JoystickButton1");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            Debug.Log("KeyCode.JoystickButton2");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            Debug.Log("KeyCode.JoystickButton3");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            Debug.Log("KeyCode.JoystickButton4");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            Debug.Log("KeyCode.JoystickButton5");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            Debug.Log("KeyCode.JoystickButton6");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Debug.Log("KeyCode.JoystickButton7");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button8))
        {
            Debug.Log("KeyCode.JoystickButton8");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            Debug.Log("KeyCode.JoystickButton9");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton10))
        {
            Debug.Log("KeyCode.JoystickButton10");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton11))
        {
            Debug.Log("KeyCode.JoystickButton11");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton12))
        {
            Debug.Log("KeyCode.JoystickButton12");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton13))
        {
            Debug.Log("KeyCode.JoystickButton13");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button14))
        {
            Debug.Log("KeyCode.JoystickButton14");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button15))
        {
            Debug.Log("KeyCode.JoystickButton15");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button16))
        {
            Debug.Log("KeyCode.JoystickButton16");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button17))
        {
            Debug.Log("KeyCode.JoystickButton17");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button18))
        {
            Debug.Log("KeyCode.JoystickButton18");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button19))
        {
            Debug.Log("KeyCode.JoystickButton19");
        }

    }

    void CreatePlayer(Controller TypeControl)
    {
        GameObject go = new GameObject();
        
        if (gameObjects.Count % 2 == 0)
        {
            go = Instantiate(player, (Vector3)(new Vector2(startPos.x - distanceBetweenX * gameObjects.Count, startPos.y)), Quaternion.identity);
        }
        else
        {
            go = Instantiate(player, (Vector3)(new Vector2(-(startPos.x - distanceBetweenX * (gameObjects.Count - 1)), startPos.y)), new Quaternion(Quaternion.identity.x, 180, Quaternion.identity.z, Quaternion.identity.w));     
        }
        
        go.GetComponent<PlayerInputHandler>().typeController = TypeControl;
        go.GetComponent<PlayerControllerLocal>().Id = gameObjects.Count;

        print("Control = " + TypeControl);
        print("Id = " + go.GetComponent<PlayerControllerLocal>().Id);
        gameObjects.Add(go);
    }
    
}
