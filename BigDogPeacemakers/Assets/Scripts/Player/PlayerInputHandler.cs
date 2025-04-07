using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
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

}
