using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float jumpForce;

    Rigidbody2D rb;
    Animator animator;
    GroundChecker groundChecker;

    private Vector2 move;
    private bool jump;
    private bool grounded;

    float timerJump;
    float cooldownJump;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundChecker = GetComponent<GroundChecker>();
    }

    public void UpdateInput(Vector2 move, bool jump)
    {
        this.move = move;
        this.jump = jump;
    }

    private void FixedUpdate()
    {
        grounded = groundChecker.IsGrounded;
        Jump();
        Move();
        if (timerJump > 0) timerJump -= Time.deltaTime;
    }
    void Jump()
    {
        if (jump && grounded && timerJump <= 0 )
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
    void Move()
    {
        
    }


}
