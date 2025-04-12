using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerAttack : MonoBehaviour
{
    private bool isAttack;

    private bool grounded;
    private bool isMelee;
    private bool isPeriod;
    private float repulsion;
    private int countTicks;
    private float cooldownTicks;

    private GameObject projectilePreFab;

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
        if(isMelee){
            repulsion = weapon.weaponData.Repulsion;
            isPeriod = weapon.weaponData.isPeriod;
            if (isPeriod)
            {
                countTicks = weapon.weaponData.CountTicks;
                cooldownTicks = weapon.weaponData.CooldownTicks;
            }
        }
        else
        {
            projectilePreFab = weapon.weaponData.projectilePreFab;
        }
        
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
            timerAttack = cooldownAttack;
            if (isMelee)
            {
                AttackMelee();
            }
            else
            {
                animator.SetTrigger("Block");
                Instantiate(projectilePreFab, transform);
            }
            
        }
    }

    void AttackMelee()
    {
        animator.SetTrigger("Attack1");
        if (weapon.enemy != null || weapon.enemy.Count != 0)
        {
            foreach (var enemy in weapon.enemy)
            {
                int damage = weapon.weaponData.Damage;
                EnemyState enemyObj;
                if (enemy.TryGetComponent<EnemyState>(out enemyObj))
                {
                    
                    if (!isPeriod) DoMeleeDamage(enemy);
                    else StartCoroutine(PeriodAttack(enemy));
                }
            }
        }
    }

    void DoMeleeDamage(GameObject enemy)
    {
        enemy.GetComponent<EnemyState>().TakeDamage(damage);
        if (repulsion != 0)
        {
            Vector3 direction = enemy.transform.position - transform.position;

            float sum = Mathf.Abs(direction.x) + Mathf.Abs(direction.y);
            Vector2 vector = new Vector2(direction.x / sum, direction.y / sum);

            enemy.GetComponent<Rigidbody2D>().AddForce(vector * repulsion, ForceMode2D.Impulse);
        }
    }

    IEnumerator PeriodAttack(GameObject enemy)
    {
        for (int i = 0; i < countTicks; i++)
        {
            DoMeleeDamage(enemy);
            yield return new WaitForSeconds(cooldownTicks);
        }
    }
}
