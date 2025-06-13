using UnityEngine;

public class AttackTurret : MonoBehaviour
{
    public GameObject projectilePreFab;

    public float timerAttack = 0;
    public float cooldownAttack = 5f;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        if (timerAttack > 0) timerAttack -= Time.deltaTime;
    }

    void Attack()
    {
        if (timerAttack <= 0)
        {
            animator.SetTrigger("Attack");
            print("Атака туррели");
            ProjectileFree bullet;
            //bullet = Instantiate(projectilePreFab, weapon.transform.position, new Quaternion(0, transform.rotation.y, 0, 0), bulletParent.transform).GetComponent<ProjectileGeometry>();
            bullet = Instantiate(projectilePreFab, new Vector2(transform.position.x, transform.position.y), transform.rotation).GetComponent<ProjectileFree>();


            bullet.owner = gameObject;
            bullet.typeTrajectory = 0;
            bullet.size = new Vector2(1, 1);
            bullet.speed = 400;
            bullet.coefficientX = 1;
            bullet.coefficientY = 1;

            bullet.targetPos = new Vector2(0, 0);
            DontDestroyOnLoad(bullet);

            timerAttack = cooldownAttack;
        }
    }
}

