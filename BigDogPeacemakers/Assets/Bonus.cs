using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class Bonus : MonoBehaviour
{
    public int bonusType;
    public float bonusFloatValue;
    public int bonusIntValue;
    public Vector2 bonusVector2;

    DynamicTipsController dynamicTipsController;

    private void Start()
    {
        dynamicTipsController = GameObject.FindGameObjectWithTag("GameController").GetComponent<DynamicTipsController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == LayerMask.GetMask("Player") && collision.gameObject.CompareTag("Player") && gameObject.GetComponent<SpriteRenderer>().enabled == true)
        {
            Interaction(collision);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Light2D>().enabled = false;
            Destroy(gameObject, 7);
        }
    }

    void Interaction(Collider2D collision)
    {
        PlayerAttack pa = collision.gameObject.GetComponent<PlayerAttack>();
        if (pa == null) { print("PlayerAttack is Null"); }
        switch(bonusType)
        {
            case 0:
                pa.ChangeSizeProj(bonusVector2);
                StartCoroutine(dynamicTipsController.CreateTip("Размер снаряда был изменен"));
                break;
            case 1:
                pa.ChangeCooldown(bonusFloatValue);
                StartCoroutine(dynamicTipsController.CreateTip("Интервал между выстрелами был изменен"));
                break;
            case 2:
                pa.ChangeCoefficientNearX(bonusFloatValue);
                StartCoroutine(dynamicTipsController.CreateTip("Коэффициент при Иксе был изменен"));
                break;
            case 3:
                pa.ChangeCoefficientNearY(bonusFloatValue);
                StartCoroutine(dynamicTipsController.CreateTip("Коэффициент при Игрике был изменен"));
                break;
            case 4:
                pa.ChangeTypeTrajectoryAttack(bonusIntValue);
                StartCoroutine(dynamicTipsController.CreateTip("Траектория движения была изменена"));
                break;
            case 5:
                pa.ChangeSpeedProjectile(bonusFloatValue);
                StartCoroutine(dynamicTipsController.CreateTip("Скорость снаряда была изменена"));
                break;
            case 6:
                pa.ChangeTypeTrajectoryAttack(Random.Range(0, 15));
                StartCoroutine(dynamicTipsController.CreateTip("Скорость снаряда была изменена"));
                break;
        }
    }

    

}
