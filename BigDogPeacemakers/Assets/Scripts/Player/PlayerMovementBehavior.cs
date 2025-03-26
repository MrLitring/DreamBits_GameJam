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
    public float movementSpeed = 500f;
    public float maxAirSpeed = 8f;
    public float jumpForce = 10f;
    int jumpCount = 0;
    int maxJumps = 1;

    private Camera mainCamera;
    private Vector2 movementDirection;
    private bool grounded;
    


    public void UpdateInputData(Vector2 vector, bool grounded)
    {
        movementDirection = vector;
        this.grounded = grounded;
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
        float x = movementDirection.x * Time.deltaTime * movementSpeed;
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
