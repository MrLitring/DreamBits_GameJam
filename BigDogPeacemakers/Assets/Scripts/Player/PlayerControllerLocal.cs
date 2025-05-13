using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerControllerLocal : MonoBehaviour
{
    PlayerInputHandler playerInputHandler;
    PlayerMovementLocal playerMovement;
    GroundChecker groundChecker;
    PlayerAttack playerAttack;
    PlayerInteract playerInteract;
    PlayerState playerState;

    bool isPlay;
    public bool isActiveMovement { get; set; }
    public int Id { get; set; }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerMovement = GetComponent<PlayerMovementLocal>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        groundChecker = GetComponentInChildren<GroundChecker>();
        playerAttack = GetComponent<PlayerAttack>();
        playerInteract = GetComponent<PlayerInteract>();
        playerState = GetComponent<PlayerState>();

    }
    private void OnEnable()
    {
        playerMovement = GetComponent<PlayerMovementLocal>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        groundChecker = GetComponentInChildren<GroundChecker>();
        playerAttack = GetComponent<PlayerAttack>();
        playerInteract = GetComponent<PlayerInteract>();
        playerState = GetComponent<PlayerState>();
    }

    private void Update()
    {
        isPlay = playerState.Health > 0 && isActiveMovement ? true : false;
        //isPlay = playerState.Health > 0 ? true : false;
        
        if (isPlay)
        {
            try
            {
                Vector2 move = playerInputHandler.GetMove();
                bool isAttack = playerInputHandler.IsAttack();
                bool isDash = playerInputHandler.IsDash();
                bool isJump = playerInputHandler.IsJump();
                bool isSprint = playerInputHandler.IsSprint();
                bool isInteract = playerInputHandler.IsInteract();
                bool isGrounded = groundChecker.IsGrounded;
                playerMovement.UpdateInputData(move, isJump, isGrounded);
                playerAttack.UpdateInputData(isAttack, isGrounded);
                playerInteract.UpdateInputData(isInteract);
            }
            catch
            {
                OnEnable();
            }
            


            //playerMovement.UpdateInputData(move, isJump, isSprint, isDash, isInteract, isGrounded);
            
        }
    }

    public void UpdateAnimator()
    {
        playerMovement.UpdateAnimator();
        playerAttack.UpdateAnimator();
        playerState.UpdateAnimator();
    }

    public void OnDisable()
    {
        var gameControllerLocal = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerLocal>();
        gameControllerLocal.DecreaseScore(Id);
    }
}
