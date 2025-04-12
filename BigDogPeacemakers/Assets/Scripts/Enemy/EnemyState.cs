using System.Collections;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public int maxHP = 3;
    public int Health { get; private set; }

    float red;
    SpriteRenderer spriteRenderer;
    Animator animator;

    float timeAnim = 2f;
    float timerAnim = 0f;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();    
        red = spriteRenderer.color.r;
        Health = maxHP;
    }
    
    public void TakeDamage(int damage)
    {
        Health = Mathf.Max(Health - damage, 0);
        print("Health Enemy = " + Health);
        if (Health == 0)
        {
            PlayDeathAnim();
        }
        StartCoroutine(PlayDamageAnim());
    }
    public void HealHealth(int heal)
    {
        Health = Mathf.Min(Health + heal, maxHP);
        print("Health Enemy = " + Health);
    }

    IEnumerator PlayDamageAnim()
    {
        for (int i = 0;i < 2;i++)
        {
            Color color = spriteRenderer.color;
            spriteRenderer.color = new Color(255, color.g, color.b, 1);
            yield return new WaitForSecondsRealtime(0.2f);
            spriteRenderer.color = new Color(red, color.g, color.b, 1);
            yield return new WaitForSecondsRealtime(0.2f);
            // timerAnim -= Time.deltaTime;
        }
        
    }
    void PlayDeathAnim()
    {
        animator.SetTrigger("Death");

        Destroy(gameObject, 1f);
    }
}
