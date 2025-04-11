using UnityEngine;

public class EnemyRun : StateMachineBehaviour
{
    public float speed = 100f;
    Transform player;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Transform weaponTransform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        sr = animator.GetComponent<SpriteRenderer>();
        weaponTransform = animator.GetComponentInChildren<Weapon>().transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (player == null) Debug.Log("Игрок не найден");
        else
        {
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, Time.deltaTime * speed);
            Rigidbody2D player_rb = player.GetComponent<Rigidbody2D>();
            

            Vector2 vec = player_rb.position.normalized;
            if (Vector2.Distance(rb.position, player_rb.position) < 3.2)
            {
                animator.SetTrigger("Attack1");
            }
            else
            {
                if (rb.position.x - player.position.x > 0)
                {
                    rb.linearVelocity = vec * -10;
                    sr.flipX = true;
                    weaponTransform.rotation = new Quaternion(0, 180, 0, 1);
                }
                else
                {
                    rb.linearVelocity = vec * 10;
                    sr.flipX = false;
                    weaponTransform.rotation = new Quaternion(0, 0, 0, 1);
                }
            }
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
