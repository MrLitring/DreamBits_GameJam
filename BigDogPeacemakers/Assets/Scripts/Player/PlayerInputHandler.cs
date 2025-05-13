using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Controller
{
    Keyboard1 = 0,
    Keyboard2 = 1,
    Gamepad1 = 2,
    Gamepad2 = 3,
    Gamepad3 = 4,
    Gamepad4 = 5
}

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 move;
    private bool jump;
    private bool sprint;
    private bool dash;
    private bool interact;
    private bool attack;

    public Vector2 GetMove() => move;
    public bool IsJump() => jump;
    public bool IsSprint() => sprint;
    public bool IsDash() => dash;
    public bool IsInteract() => interact;
    public bool IsAttack() => attack;
    /*
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started) jump = true; 
        else if (context.canceled) jump = false;
        
    }
    public void OnDash(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            dash = true;
        }
        else if (context.canceled)
        {
            dash = false;
        }
        

    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed || context.started)
        {
            sprint = true;
        }
        else if (context.canceled)
        {
            sprint = false;
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started) interact = true;
        else if (context.canceled) interact = false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            attack = true;
        }
        else if (context.canceled)
        {
            attack = false;
        }
    }
    */

    string hor;
    string vert;
    KeyCode jumpButton;
    KeyCode attackButton;
    KeyCode rightButton;
    KeyCode leftButton;
    KeyCode upButton;
    KeyCode downButton;
    public Controller typeController;
    
    private void Start()
    {
        var gameObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

    }

    /*
    private void Update()
    {
        
        jumpButton = KeyCode.Space;
        attackButton = KeyCode.H;
        


        if (Input.GetKey(jumpButton))
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

        
        float x = 0;
        float y = 0;
        if (Input.GetKey(KeyCode.W))
        {
            y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            x = -1;
        }

        move = new Vector2(x, y);
        
        if (Input.GetKey(attackButton))
        {
            attack = true;
            print("Атака");
        }
        else
        {
            attack = false;
        }
    }*/
    
    private void Update()
    {

        switch (typeController)
        {
            case Controller.Keyboard1:
                jumpButton = KeyCode.Space;
                attackButton = KeyCode.H;
                rightButton = KeyCode.D;
                leftButton = KeyCode.A;
                upButton = KeyCode.W;
                downButton = KeyCode.S;
                break;
            case Controller.Keyboard2:
                jumpButton = KeyCode.Keypad0;
                attackButton = KeyCode.KeypadPeriod;
                rightButton = KeyCode.RightArrow;
                leftButton = KeyCode.LeftArrow;
                upButton = KeyCode.UpArrow;
                downButton = KeyCode.DownArrow;
                break;
            case Controller.Gamepad1:
                hor = "HorizontalJoy1";
                vert = "VerticalJoy1";
                jumpButton = KeyCode.Joystick1Button5;
                attackButton = KeyCode.Joystick1Button1;
                break;
            case Controller.Gamepad2:
                hor = "HorizontalJoy2";
                vert = "VerticalJoy2";
                jumpButton = KeyCode.Joystick2Button5;
                attackButton = KeyCode.Joystick2Button1;
                break;
            case Controller.Gamepad3:
                hor = "HorizontalJoy3";
                vert = "VerticalJoy3";
                jumpButton = KeyCode.Joystick3Button5;
                attackButton = KeyCode.Joystick3Button1;
                break;
            case Controller.Gamepad4:
                hor = "HorizontalJoy4";
                vert = "VerticalJoy4";
                jumpButton = KeyCode.Joystick4Button5;
                attackButton = KeyCode.Joystick4Button1;
                break;
        }
        
        

        if (Input.GetKey(jumpButton))
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

        if (typeController != Controller.Keyboard1 && typeController != Controller.Keyboard2)
        {
            move = new Vector2(Input.GetAxis(hor), Input.GetAxis(vert));
        }
        else
        {
            float x = 0;
            float y = 0;
            if (Input.GetKey(upButton))
            {
                y = 1;
            }else if (Input.GetKey(downButton))
            {
                y = -1;
            }
            if (Input.GetKey(rightButton))
            {
                x = 1;
            }
            else if (Input.GetKey(leftButton))
            {
                x = -1;
            }

            move = new Vector2(x, y);
        }
        if (Input.GetKey(attackButton))
        {
            attack = true;
        }
        else
        {
            attack = false;
        }

    }



}
