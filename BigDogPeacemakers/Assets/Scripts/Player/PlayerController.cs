using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputHandler playerInputHandler;
    PlayerMovement playerMovement;
    GroundChecker groundChecker;
    PlayerAttack playerAttack;
    PlayerInteract playerInteract;

    // Добавить инвентарь?
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        groundChecker = GetComponent<GroundChecker>();
        playerAttack = GetComponent<PlayerAttack>();
        playerInteract = GetComponent<PlayerInteract>();
    }

    private void Update()
    {
        Vector2 move = playerInputHandler.GetMove();
        bool isAttack = playerInputHandler.IsAttack();
        bool isDash = playerInputHandler.IsDash();
        bool isJump = playerInputHandler.IsJump();
        bool isSprint = playerInputHandler.IsSprint();
        bool isInteract = playerInputHandler.IsInteract();
        bool isGrounded = groundChecker.IsGrounded;
        if (isJump) print("Jump");
        if (isSprint) print("Sprint");
        if (isDash) print("Dash");
        if (isGrounded) print("Grounded"); else print("Not Grounded");
        if (isInteract) print("Interact");
        if (isAttack) print("Attack");


        playerMovement.UpdateInputData(move, isJump, isSprint, isDash, isInteract, isGrounded);
        playerAttack.UpdateInputData(isAttack, isGrounded);
        playerInteract.UpdateInputData(isInteract);
    }


}
