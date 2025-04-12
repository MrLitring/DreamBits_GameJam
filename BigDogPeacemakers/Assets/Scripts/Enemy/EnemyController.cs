using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool isGrounded;
    private bool isTouchWall;

    private GroundChecker groundChecker;
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private DistanceBetween distance;

    PlayerCheker playerCheker;
    WallChecker wallChecker;

    public bool isEnemyActive;

    void Start()
    {
        groundChecker = GetComponent<GroundChecker>();
        enemyMovement = GetComponent<EnemyMovement>();
        playerCheker = GetComponentInParent<PlayerCheker>();
        wallChecker = GetComponentInChildren<WallChecker>();
        distance = GetComponent<DistanceBetween>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    
    void Update()
    {
        isGrounded = groundChecker.IsGrounded;
        isTouchWall = wallChecker.WallIsHere;
        isEnemyActive = playerCheker.isPlayer;
        enemyMovement.UpdateInput(new Vector2(), isTouchWall, isGrounded, isEnemyActive);
        if (distance.distance < 3.5f) enemyAttack.UpdateInput(true);
        else enemyAttack.UpdateInput(false);
        
    }
}
