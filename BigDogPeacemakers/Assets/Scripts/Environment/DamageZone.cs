using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damage = 1;
    float timer;
    float cooldown;
    private void Start()
    {
        cooldown = 0.5f;
        timer = 0;
    }
    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == LayerMask.GetMask("Player") && timer <= 0)
        {
            collision.GetComponent<PlayerState>().TakeDamage(damage);
            timer = cooldown;
        }
    }
}
