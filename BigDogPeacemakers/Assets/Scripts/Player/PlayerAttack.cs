using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerAttack : MonoBehaviour
{
    private bool isAttack;
    private bool grounded;
    private bool isMelee;
    private Animator animator;
    private float timerAttack;
    private float cooldownAttack;
    private int damage;
    private Weapon weapon;
    void Start()
    {
        weapon = gameObject.GetComponentInChildren<Weapon>();
        animator = GetComponent<Animator>();
        cooldownAttack = weapon.weaponData.AttackSpeed;
        damage = weapon.weaponData.Damage;
        isMelee = weapon.weaponData.isMelee;
        timerAttack = 0;
    }

    
    void FixedUpdate()
    {
        Attack();
        if (timerAttack > 0) timerAttack -= Time.deltaTime;
    }
    public void UpdateInputData(bool isAttack, bool grounded)
    {
        this.isAttack = isAttack;
        this.grounded = grounded;
    }

    void Attack()
    {

        if (isAttack && timerAttack <= 0)
        {
            if (isMelee)
            {
                animator.SetTrigger("Attack1");
                if (weapon.enemy != null || weapon.enemy.Count != 0)
                {
                    foreach (var enemy in weapon.enemy)
                    {
                        TestStaticObj testStaticObj = enemy.GetComponent<TestStaticObj>();
                        testStaticObj.TakeDamage(1);
                    }
                }
            }
            else
            {
                animator.SetTrigger("Block");
                timerAttack = cooldownAttack;
                Instantiate(weapon.weaponData.projectilePreFab, transform);
            }
            timerAttack = cooldownAttack;
        }
    }
}
