using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool isGrounded;

    private GroundChecker groundChecker;
    private EnemyAnimation enemyAnimation;

    PlayerCheker playerCheker;

    void Start()
    {
        groundChecker = GetComponent<GroundChecker>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        playerCheker = GetComponentInParent<PlayerCheker>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = groundChecker.IsGrounded;
        enemyAnimation.UpdateData(isGrounded);
    }
}
