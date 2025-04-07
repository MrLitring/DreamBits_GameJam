using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.LightAnchor;

public class PlayerMovementBehavior : MonoBehaviour
{


    [Header("Component References")]
    public Rigidbody2D playerRigidbody;
    public Transform groundCollider;

    [Header("Movement Settings")]
    [SerializeField] float movementSpeed = 500f;
    [SerializeField] float maxAirSpeed = 8f;
    [SerializeField] float jumpForce = 35f;
    int jumpCount = 0;
    int maxJumps = 1;
    float acceleration;
    private Camera mainCamera;
    private Vector2 movementDirection;
    private bool grounded;
    


    public void UpdateInputData(Vector2 vector, bool grounded, float accelerationPercent)
    {
        movementDirection = vector;
        this.grounded = grounded;
        this.acceleration = 1 + accelerationPercent;
    }

    

    void FixedUpdate()
    {
        MovePlayer();

        if (!grounded)
        {
            // Ограничиваем скорость по X
            float clampedX = Mathf.Clamp(playerRigidbody.linearVelocityX, -maxAirSpeed, maxAirSpeed);
            playerRigidbody.linearVelocityX = clampedX;
        }

    }

    void MovePlayer()
    {
        Transform playerTransform = playerRigidbody.GetComponent<Transform>();
        float x = movementDirection.x * Time.deltaTime * movementSpeed * acceleration;
        float y = playerTransform.position.y;
        if (grounded)
        {
            playerRigidbody.linearVelocityX = x;
        }
        else
        {
            playerRigidbody.AddForceX(x / 1.5f);
        }
    }   

    public void Jump(bool flag)
    {
        if (flag)
        {
            var stayJump = new Vector2(0, jumpForce);
            var jumpDirection = stayJump + movementDirection;
            //playerRigidbody.AddForce(jumpDirection, ForceMode2D.Impulse);
            playerRigidbody.AddForce(stayJump, ForceMode2D.Impulse);
            
            print("Jump " + movementDirection.ToSafeString());
        }
        
    }
}
