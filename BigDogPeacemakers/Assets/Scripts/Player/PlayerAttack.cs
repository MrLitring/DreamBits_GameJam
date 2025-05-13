using System;
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

    

    private Animator animator;
    private float timerAttack;
    private float cooldownAttack;
    private int damage;
    private Weapon weapon;

    //Data Bullet
    private GameObject projectilePreFab;
    GameObject bulletParent;
    public int typeTrajectory = 0;
    public float speedProjectile = 5f;
    public Vector2 sizeProjectile = new Vector2(0.1f, 0.1f);
    public float coefficientX = 1;
    public float coefficientY = 1;

    void Start()
    {
        weapon = gameObject.GetComponentInChildren<Weapon>();
        animator = animator = transform.Find("VisContainer").gameObject.GetComponentInChildren<Animator>();
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

    private void OnEnable()
    {
        weapon = gameObject.GetComponentInChildren<Weapon>();
    }

    public void UpdateAnimator()
    {
        animator = animator = transform.Find("VisContainer").gameObject.GetComponentInChildren<Animator>();
    }

    private void OnLevelWasLoaded(int level)
    {
        bulletParent = GameObject.FindGameObjectWithTag("SpawnBullets");
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

    public void ChangeTypeTrajectoryAttack(int TypeTrajectory)
    {
        typeTrajectory = TypeTrajectory;
    }

    public void ChangeCoefficientNearX(float x)
    {
        coefficientX += x;
    }
    public void ChangeCoefficientNearY(float y)
    {
        coefficientY += y;
    }

    public void ChangeSizeProj(Vector2 vector)
    {
        sizeProjectile = vector;
    }

    public void ChangeCooldown(float cooldown)
    {
        cooldownAttack = cooldown;
    }
    public void ChangeSpeedProjectile(float speed)
    {
        speedProjectile = speed;
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
                try
                {
                    animator.SetTrigger("Attack");
                }
                catch
                {
                    UpdateAnimator();
                }
                

                
                try
                {
                    ProjectileGeometry bullet;
                    //bullet = Instantiate(projectilePreFab, weapon.transform.position, new Quaternion(0, transform.rotation.y, 0, 0), bulletParent.transform).GetComponent<ProjectileGeometry>();
                    bullet = Instantiate(projectilePreFab, weapon.transform.position, new Quaternion(0, transform.rotation.y, 0, 0)).GetComponent<ProjectileGeometry>();
                    bool isleft = GetComponent<PlayerMovementLocal>().isLeft;
                    bullet.isLeft = isleft;

                    bullet.owner = gameObject;
                    bullet.typeTrajectory = typeTrajectory;
                    bullet.size = sizeProjectile;
                    bullet.speed = speedProjectile;
                    bullet.coefficientX = coefficientX;
                    bullet.coefficientY = coefficientY;
                    DontDestroyOnLoad(bullet);
                }
                catch(Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
                
                
            }
            isAttack = false;
            
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
