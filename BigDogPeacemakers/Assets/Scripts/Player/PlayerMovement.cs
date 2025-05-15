using System.Collections;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{

   
    // Dynamic Variables
    
    //private Vector2 move;
    //private bool jump;
    private bool sprint;
    private bool dash;
    private bool interact;
    private bool attack;
    //private bool grounded;

    NetworkVariable<Vector2> move = new NetworkVariable<Vector2>();
    NetworkVariable<bool> jump = new NetworkVariable<bool>();
    NetworkVariable<bool> grounded = new NetworkVariable<bool>();
    NetworkVariable<float> timerJump = new NetworkVariable<float>();

    public bool isLeft { get; private set; }

    // Static Variables
    public float movementSpeed = 500f;

    public float jumpForce = 35f;
    float acceleration = 1.5f;
    Rigidbody2D rb;
    Animator animator;

    PolygonCollider2D polygonCollider2D;
    float timerDash;
    float cooldownDash;

    //float timerJump;
    float cooldownJump;


    Weapon weapon;


    private void Start()
    {
        weapon = gameObject.GetComponentInChildren<Weapon>();


        rb = GetComponent<Rigidbody2D>();
        
        animator = transform.Find("Visual").gameObject.GetComponent<Animator>();//;.GetComponent<Animator>();


        cooldownDash = 2.0f;
        cooldownJump = 0.5f;
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        
        if (transform.rotation.y == 0)
        {
            isLeft = false;
        }
        else
        {
            isLeft = true;
        }
    }


    /*
    public void UpdateInputData(Vector2 move, bool jump, bool sprint, bool dash, bool interact,  bool grounded)
    {
        this.move = move;
        this.jump = jump;
        this.sprint = sprint;
        this.dash = dash;
        this.interact = interact;
        
        this.grounded = grounded;
    }
    */

    [ServerRpc(RequireOwnership = false)]
    public void UpdateInputDataServerRpc(Vector2 move, bool jump, bool grounded)
    {
        this.move.Value = move;
        this.jump.Value = jump;
        this.grounded.Value = grounded;
        
    }


    private void FixedUpdate()
    {
        if (IsOwner)
        {
            print(this.jump.Value);
            MovePlayerServerRpc();
            JumpServerRpc();
            print("Move " + move.Value);
        
            animator.SetFloat("AirSpeedY", rb.linearVelocityY);
            animator.SetBool("Grounded", grounded.Value);
            if (timerDash > 0) timerDash -= Time.deltaTime;

            if (timerJump.Value > 0) TimerDownServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void TimerDownServerRpc()
    {
        timerJump.Value -= Time.deltaTime;
    }

    [ClientRpc]
    void MovePlayerClientRpc()
    {
        float x;
        float moveX = move.Value.x;
        print("MoveX = " + moveX);
        if (moveX > 0.5f) moveX = 1;
        else if (moveX < 0.5f && moveX >= 0) moveX = 0;
        else if (moveX < 0) moveX = -1;

        if (sprint) x = moveX * Time.deltaTime * movementSpeed * acceleration;
        else x = moveX * Time.deltaTime * movementSpeed;

        if (grounded.Value)
        {
            rb.linearVelocityX = x;
        }
        else
        {
            rb.AddForceX(x / 1.5f);
        }

        if (x != 0)
        {
            animator.SetInteger("AnimState", 1);
        }
        else animator.SetInteger("AnimState", 0);

        if (x < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            isLeft = true;
        }
        else if (x > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            isLeft = false;
        }
    }


    [ServerRpc(RequireOwnership = false)]
    void MovePlayerServerRpc()
    {
        MovePlayerClientRpc();
    }


    [ServerRpc(RequireOwnership = false)]
    void JumpServerRpc()
    {
        if (jump.Value && grounded.Value && timerJump.Value <= 0 && !dash)
        {
            rb.linearVelocityY = 0;
            var stayJump = new Vector2(0, jumpForce);
            var jumpDirection = stayJump + move.Value;
            rb.AddForce(stayJump, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
            
            timerJump.Value = cooldownJump;
            jump.Value = false;
        }
        else
        {
            //print($"jump.Value = {jump.Value} && grounded.Value = {grounded.Value} && timerJump = {timerJump.Value} <= 0 && !dash = {!dash}");
        }
    }
    /*
    void Dash()
    {
        if (grounded && dash && timerDash <= 0 && !jump.Value)
        {
            animator.SetTrigger("Roll");
            if (isLeft)
            {
                rb.AddForce(new Vector2(200, 0) * -1, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(200, 0), ForceMode2D.Impulse);
            }
            
            timerDash = cooldownDash;
        }
    }
    */
}
