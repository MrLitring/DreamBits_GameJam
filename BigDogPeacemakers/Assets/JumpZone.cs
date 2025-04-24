using UnityEngine;

public class JumpZone : MonoBehaviour
{
    public float jumpForce = 50f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == LayerMask.GetMask("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForceY(jumpForce, ForceMode2D.Impulse);
        }
    }
}
