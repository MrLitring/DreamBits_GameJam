using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Dynamic Variables
    private Vector2 move;
    private bool jump;
    private bool sprint;
    private bool dash;
    private bool interact;
    private bool attack;
    private bool grounded;
    private bool isLeft;

    // Static Variables
    float movementSpeed = 500f;
    //float maxAirSpeed = 8f;
    float jumpForce = 35f;
    float acceleration = 1.5f;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    float timerDash;
    float cooldownDash;

    float timerAttack;
    


    Weapon weapon;


    private void Start()
    {
        weapon = gameObject.GetComponentInChildren<Weapon>();


        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        cooldownDash = 2.0f;
        isLeft = spriteRenderer.flipX;

    }





    public void UpdateInputData(Vector2 move, bool jump, bool sprint, bool dash, bool interact,  bool grounded)
    {
        this.move = move;
        this.jump = jump;
        this.sprint = sprint;
        this.dash = dash;
        this.interact = interact;
        
        this.grounded = grounded;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        Jump();
        Dash();
        
        SpinWeaponCollider();
        //Fall();
        animator.SetFloat("AirSpeedY", rb.linearVelocityY);
        animator.SetBool("Grounded", grounded);
        if (timerDash > 0) timerDash -= Time.deltaTime;
        if (timerAttack > 0) timerAttack -= Time.deltaTime;
    }



    void MovePlayer()
    {
        float x;
        if (move.x > 0.5f) move.x = 1;
        else if (move.x < 0.5f && move.x >= 0) move.x = 0;
        else if(move.x < 0) move.x = -1;

        if (sprint) x = move.x * Time.deltaTime * movementSpeed * acceleration;
        else x = move.x * Time.deltaTime * movementSpeed;

        if (grounded)
        {
            rb.linearVelocityX = x;
        }
        else
        {
            rb.AddForceX(x / 1.5f);
        }

        if (x != 0) animator.SetInteger("AnimState", 1);
        else animator.SetInteger("AnimState", 0);

        if (x < 0)
        {
            isLeft = true;
            spriteRenderer.flipX = isLeft;
        }
        else if (x > 0)
        {
            isLeft = false;
            spriteRenderer.flipX = isLeft;

        }
    }

    void SpinWeaponCollider()
    {
        if (isLeft)
        {
            weapon.transform.rotation = new Quaternion(0, 180, 0, 1);
        }
        else
        {
            weapon.transform.rotation = new Quaternion(0, 0, 0, 1);
        }
    }

    void Jump()
    {
        if (jump && grounded)
        {
            rb.linearVelocityY = 0;
            var stayJump = new Vector2(0, jumpForce);
            var jumpDirection = stayJump + move;
            rb.AddForce(stayJump, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
            jump = false;
        }
    }

    void Dash()
    {
        if (grounded && dash && timerDash <= 0)
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
    
}
