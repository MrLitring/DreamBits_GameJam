using UnityEngine;

public class EnemyRun : StateMachineBehaviour
{
    public float speed = 100f;
    Transform player;
    Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (player == null) Debug.Log("Игрок не найден");
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        //Debug.Log("Целевой вектор" + target);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, Time.deltaTime * speed);
        Debug.Log("Новый вектор" + target);
        //rb.MovePosition(newPos);
        rb.linearVelocity = (new Vector2(-newPos.x, player.position.y));
        //rb.linearVelocityX = Mathf.Min(30, Mathf.Abs(rb.linearVelocity.x));
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
