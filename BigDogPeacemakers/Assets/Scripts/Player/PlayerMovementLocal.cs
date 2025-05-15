using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovementLocal : MonoBehaviour
{
    // Dynamic Variables

    private Vector2 move;
    private bool jump;
    private bool sprint;
    private bool dash;
    private bool interact;
    private bool attack;
    private bool grounded;

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

    float timerJump;
    float cooldownJump;

    Weapon weapon;


    private void Start()
    {
        weapon = gameObject.GetComponentInChildren<Weapon>();


        rb = GetComponent<Rigidbody2D>();

        animator = transform.Find("VisContainer").gameObject.GetComponentInChildren<Animator>();//;.GetComponent<Animator>();


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

    public void UpdateAnimator()
    {
        animator = transform.Find("VisContainer").gameObject.GetComponentInChildren<Animator>();
    }

    public void UpdateInputData(Vector2 move, bool jump, bool grounded)
    {
        this.move = move;
        this.jump = jump;
        this.grounded = grounded;
    }


    private void FixedUpdate()
    {
        MovePlayer();
        Jump();

        animator.SetFloat("AirSpeedY", rb.linearVelocityY);
        animator.SetBool("Grounded", grounded);
        if (timerDash > 0) timerDash -= Time.deltaTime;

        if (timerJump > 0) timerJump -= Time.deltaTime;
    }

   


    void MovePlayer()
    {
        float x;
        float moveX = move.x;
        if (moveX > 0.5f) moveX = 1;
        else if (moveX < 0.5f && moveX >= 0) moveX = 0;
        else if (moveX < 0) moveX = -1;

        if (sprint) x = moveX * Time.deltaTime * movementSpeed * acceleration;
        else x = moveX * Time.deltaTime * movementSpeed;
        if (grounded)
        {
            rb.linearVelocityX = x;
        }
        else
        {
            rb.AddForceX(x / 1.5f);
        }
        try
        {
            if (x != 0)
            {
                animator.SetInteger("AnimState", 1);
            }
            else animator.SetInteger("AnimState", 0);

        }
        catch
        {
            UpdateAnimator();
        }

        if (move.x < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            isLeft = true;
        }
        else if (move.x > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            isLeft = false;
        }
    }



    void Jump()
    {
        if (jump && grounded && timerJump <= 0 && !dash)
        {
            rb.linearVelocityY = 0;
            var stayJump = new Vector2(0, jumpForce);
            var jumpDirection = stayJump + move;
            rb.AddForce(stayJump, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");

            timerJump = cooldownJump;
            jump = false;
        }
        
    }
    
}
