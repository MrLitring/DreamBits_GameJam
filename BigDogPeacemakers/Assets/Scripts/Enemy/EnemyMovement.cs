using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float jumpForce = 30;
    public float speed = 10;

    Rigidbody2D rb;
    Animator animator;
    GroundChecker groundChecker;
    Transform player;
    Transform weaponMelee;
    SpriteRenderer sr;

    private Vector2 move;
    private bool jump;
    private bool grounded;
    private bool isActiveMove;

    float timerJump = 0;
    float cooldownJump = 2f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundChecker = GetComponent<GroundChecker>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        weaponMelee = GetComponentInChildren<Weapon>().transform;
        sr = GetComponent<SpriteRenderer>();
    }

    public void UpdateInput(Vector2 move, bool jump, bool grounded, bool isActiveMove)
    {
        this.move = move;
        this.jump = jump;
        this.grounded = grounded;
        this.isActiveMove = isActiveMove;
    }

    private void FixedUpdate()
    {
        animator.SetBool("Grounded", grounded);
        if (isActiveMove)
        {
            animator.SetBool("PlayerHere", true);
            grounded = groundChecker.IsGrounded;
            Jump();
            Move();
            if (timerJump > 0) timerJump -= Time.deltaTime;
            animator.SetFloat("AirSpeedY", rb.linearVelocityY);
        }
    }
    void Jump()
    {
        if (jump && grounded && timerJump <= 0 && player.position.y > transform.position.y)
        {
            rb.linearVelocityY = 0;
            var stayJump = new Vector2(0, jumpForce);
            //var jumpDirection = stayJump + move;
            rb.AddForce(stayJump, ForceMode2D.Impulse);

            animator.SetTrigger("Jump");
            timerJump = cooldownJump;
            jump = false;
            
        }
    }
    void Move()
    {
        if (player == null) Debug.Log("Игрок не найден");
        else
        {
            animator.SetInteger("AnimState", 1);
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, Time.deltaTime * speed);
            Rigidbody2D player_rb = player.GetComponent<Rigidbody2D>();


            Vector2 vec = player_rb.position.normalized;
            
            if (rb.position.x - player.position.x > 0)
            {
                rb.linearVelocityX = vec.x * -10;
                sr.flipX = true;
                weaponMelee.rotation = new Quaternion(0, 180, 0, 1);
            }
            else
            {
                rb.linearVelocityX = vec.x * 10;
                sr.flipX = false;
                weaponMelee.rotation = new Quaternion(0, 0, 0, 1);
            }
            
        }
    }


}
