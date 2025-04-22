using UnityEngine;

public class Bonus : MonoBehaviour
{
    public int bonusType;
    public float bonusFloatValue;
    public int bonusIntValue;
    public Vector2 bonusVector2;
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == LayerMask.GetMask("Player"))
        {
            Interaction(collision);
            Destroy(gameObject);
        }
    }

    void Interaction(Collider2D collision)
    {
        PlayerAttack pa = collision.gameObject.GetComponent<PlayerAttack>();
        switch(bonusType)
        {
            case 0:
                pa.ChangeSizeProj(bonusVector2);
                break;
            case 1:
                pa.ChangeCooldown(bonusFloatValue);
                break;
            case 2:
                pa.ChangeCoefficientNearX(bonusFloatValue);
                break;
            case 3:
                pa.ChangeCoefficientNearY(bonusFloatValue);
                break;
            case 4:
                pa.ChangeTypeTrajectoryAttack(bonusIntValue);
                break;
            case 5:
                pa.ChangeSpeedProjectile(bonusFloatValue);
                break;
        }
    }
}
