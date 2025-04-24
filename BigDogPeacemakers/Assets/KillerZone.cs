using UnityEngine;

public class KillerZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == LayerMask.GetMask("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}
