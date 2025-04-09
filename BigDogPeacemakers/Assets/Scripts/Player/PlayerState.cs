using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    int maxHP = 5;
    float timerInvincibility;
    public int Health {  get; private set; }
    public float timeInvincibility {  get; private set; }

    Animator animator;


    void Start()
    {
        Health = maxHP;
        timeInvincibility = 1f;
        timerInvincibility = 0f;
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (timerInvincibility > 0f) timerInvincibility -= Time.deltaTime;
        StartCoroutine(PlayInvincibilityAnim());
    }

    public void TakeDamage(int damage)
    {
        if (timerInvincibility <= 0f)
        {
            Health = Mathf.Max(Health - damage, 0);
            print("Health = " + Health);
            timerInvincibility = timeInvincibility;
            if (Health <= 0) PlayDeathAnim();
        }
    }
     public void HealHealth(int heal)
    {
        
        Health = Mathf.Min(Health + heal, maxHP);
        print("Health = " + Health);
        
    }

    void PlayDeathAnim()
    {
        animator.SetTrigger("Death");
        
        Destroy(gameObject, 1f);
    }

    IEnumerator PlayInvincibilityAnim()
    {
        if (timerInvincibility > 0f)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            Color color = spriteRenderer.color;
            spriteRenderer.color =  new Color(color.r, color.g, color.b, 0);
            yield return new WaitForSecondsRealtime(0.1f);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 1);
            yield return new WaitForSecondsRealtime(0.1f);
        }


    }


}
