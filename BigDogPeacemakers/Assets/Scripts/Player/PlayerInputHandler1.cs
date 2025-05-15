using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputHandler1 : MonoBehaviour
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

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            move = new Vector2(-1, 0);
        }else if (Input.GetKey(KeyCode.D))
        {
            move = new Vector2(1, 0);
        }
        else
        {
            move = new Vector2(0, 0);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

    }

}
