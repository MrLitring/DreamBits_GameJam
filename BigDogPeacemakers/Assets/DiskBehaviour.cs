using UnityEngine;

public class DiskBehaviour : MonoBehaviour
{

    public float speedRotation;
    float timer = 0;
    public float cooldown;
    public float damage;
    public float punchForce;

    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Rotate();
        if (timer > 0) timer -= Time.deltaTime;
    }

    

    void Rotate()
    {
        if (timer <= 0)
        {
            rb.AddTorque(speedRotation,ForceMode2D.Impulse);
            timer = cooldown;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rbObject;
        if (collision.TryGetComponent<Rigidbody2D>(out rbObject))
        {
            Vector2 direction = (rbObject.position - rb.position).normalized;

            rbObject.AddForce(direction * punchForce * rb.angularVelocity, ForceMode2D.Impulse);
            PlayerState ps;
            if (damage > 0 && rbObject.TryGetComponent<PlayerState>(out ps))
            {
                ps.TakeDamage(damage);
            }
        }
    }
}
