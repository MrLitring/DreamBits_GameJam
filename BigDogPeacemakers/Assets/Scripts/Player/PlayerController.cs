using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    PlayerInputHandler playerInputHandler;
    PlayerMovement playerMovement;
    GroundChecker groundChecker;
    PlayerAttack playerAttack;
    PlayerInteract playerInteract;
    PlayerState playerState;

    bool isPlay;
    public bool isActiveMovement { get; set; }
    
    // Добавить инвентарь?
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (!IsOwner) return;
        playerMovement = GetComponent<PlayerMovement>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        groundChecker = GetComponentInChildren<GroundChecker>();
        playerAttack = GetComponent<PlayerAttack>();
        playerInteract = GetComponent<PlayerInteract>();
        playerState = GetComponent<PlayerState>();
        NetworkObject no = GetComponent<NetworkObject>();

    }
    
    private void Update()
    {
        if (!IsOwner) return;
        
        

        isPlay = playerState.Health > 0 && isActiveMovement ? true : false;

        if (isPlay)
        {
            Vector2 move = playerInputHandler.GetMove();
            bool isAttack = playerInputHandler.IsAttack();
            bool isDash = playerInputHandler.IsDash();
            bool isJump = playerInputHandler.IsJump();
            bool isSprint = playerInputHandler.IsSprint();
            bool isInteract = playerInputHandler.IsInteract();
            bool isGrounded = groundChecker.IsGrounded;


            //playerMovement.UpdateInputData(move, isJump, isSprint, isDash, isInteract, isGrounded);
            playerMovement.UpdateInputDataServerRpc(move, isJump, isGrounded);
            print("Control Move " + move);
            playerAttack.UpdateInputData(isAttack, isGrounded);
            playerInteract.UpdateInputData(isInteract);
        }


    }


}
