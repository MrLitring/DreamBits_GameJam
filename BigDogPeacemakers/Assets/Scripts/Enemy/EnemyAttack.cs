using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Animator animator;
    Weapon weapon;
    Collider2D colliderWeapon;
    bool isAttack;
    float timerAttack;
    float cooldownAttack;
    bool isMelee;
    void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
        animator = GetComponent<Animator>();
        isMelee = weapon.weaponData.isMelee;
        colliderWeapon = weapon.GetComponent<Collider2D>();
        cooldownAttack = weapon.weaponData.AttackSpeed;
    }


    // Update is called once per frame
    void Update()
    {
        if (timerAttack > 0) timerAttack -= Time.deltaTime; 
    }


    public void Attack()
    {

        if (timerAttack <= 0)
        {
            if (isMelee)
            {
                animator.SetTrigger("Attack1");
                List<Collider2D> colliders = new List<Collider2D>();
                colliderWeapon.Overlap(colliders);
                foreach (Collider2D col in colliders)
                    if (col.CompareTag("Player"))
                    {
                        int damage = weapon.weaponData.Damage;
                        PlayerState playerObj;
                        if (col.TryGetComponent<PlayerState>(out playerObj))
                        {
                            playerObj.TakeDamage(damage);
                        }
                    }
            }
            else
            {
                animator.SetTrigger("Block");
                //Instantiate(weapon.weaponData.projectilePreFab, transform);
            }
            timerAttack = cooldownAttack;
        }
    }

}
