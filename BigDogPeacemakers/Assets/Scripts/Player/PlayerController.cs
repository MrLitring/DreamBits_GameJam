using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private Vector2 newPos;
    private SpriteRenderer spriteRenderer;
    private bool grounded;
    private Rigidbody2D rb;

    public LayerMask groundLayer;
    Animator animator;
    public PlayerMovementBehavior playerMovementBehavior;

    

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = false;
        animator = GetComponent<Animator>();    
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        newPos = value.Get<Vector2>();
        
        if (newPos.x > 0)
        {
            spriteRenderer.flipX = false;
            
            animator.SetInteger("AnimState", 1);

        }
        else if (newPos.x < 0) 
        { 
            spriteRenderer.flipX = true;
            animator.SetInteger("AnimState", 1);
          
        }
        else
        {
            animator.SetInteger("AnimState", 0);
        }
    }
    
    public void OnJump(InputValue value)
    {
        float val= value.Get<float>();
        if (grounded)
        {
            playerMovementBehavior.Jump(grounded);
            animator.SetTrigger("Jump");
        }
    }
    public void OnDash()
    {
        if (grounded)
        {
            animator.SetTrigger("Roll");
        }
    }


    private void Update()
    {
        playerMovementBehavior.UpdateInputData(newPos, grounded);
        animator.SetFloat("AirSpeedY", rb.linearVelocity.y);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        LayerMask layerMask = 1 << collision.gameObject.layer;

        if (layerMask == groundLayer)
        {
            grounded = true;
            animator.SetBool("Grounded", grounded);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        LayerMask layerMask = 1 << collision.gameObject.layer;

        if (layerMask == groundLayer)
        {
            grounded = false;
            animator.SetBool("Grounded", grounded);
        }
        
    }

}
