using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private bool isGrounded;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateData(bool isGrounded)
    {
        this.isGrounded = isGrounded;
    }

    void Update()
    {
        animator.SetBool("Grounded", isGrounded);
        //animator.SetInteger("AnimState", 1);
    }
}
