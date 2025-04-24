using UnityEngine;
//using System.Linq.Dynamic.Core;

public class ProjectileGeometry : MonoBehaviour
{

    


    float x0;
    float y0;
    public int damage;
    public float lifetime;
    //public float speed;
    public float range;
    public string formula;



    
    public float heightFactor = 2f; // Чем больше — тем выше дуга
    private Vector2 startPoint;
    private Vector2 direction; // Нормализованное направление
    private float distanceTravelled = 0;
    



    //Input Data
    public int typeTrajectory;
    public bool isLeft;
    public GameObject owner;
    public Vector2 size;
    public float speed;
    public float coefficientY;
    public float coefficientX;

    void Start()
    {
        startPoint = transform.position;
        Vector2 targetMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (targetMouse - startPoint).normalized;
        transform.localScale = size;
        if (isLeft)
        {
            speed = -speed;
        }
        Destroy(gameObject, 90f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLeft)
        {
            speed = -Mathf.Abs(speed);
        }
        distanceTravelled += speed * Time.deltaTime;
        float x;
        
        x = distanceTravelled;
        //float y = Mathf.Pow((x), 2);
        float y = CalculateY(x);
        Vector2 localOffset = new Vector2(x, y);

        transform.position = startPoint + localOffset;// rotated;

    }
    

    float CalculateY(float x)
    {
        float y;
        y = Mathf.Sqrt(Mathf.Abs(x));
        switch (typeTrajectory)
        {
            case 0:
                y = coefficientY * (x * coefficientX); break;
            case 1:
                y = coefficientY * (x * coefficientX) * (x * coefficientX); break;
            case 2:
                y = coefficientY * Mathf.Pow((x * coefficientX), 1 / 3);
                break;
            case 3:
                y = coefficientY * Mathf.Pow(2, (x * coefficientX)); break;
            case 4:
                y = coefficientY * Mathf.Pow(0.5f, (x * coefficientX));
                break;
            case 5:
                y = coefficientY * (- (x * coefficientX)); break;
            case 6:
                y = -((x * coefficientX) * (x * coefficientX)); break;
            case 7:
                y = coefficientY *  0.5f * Mathf.Pow(2, (x * coefficientX)); break;
            case 8:
                y = coefficientY * Mathf.Sin((x * coefficientX)); break;
            case 9:
                y = coefficientY * Mathf.Cos((x * coefficientX)); break;
            case 10:
                y = Mathf.Clamp(coefficientY * Mathf.Tan((x * coefficientX)), -5, 5); break;
            case 11:
                y = coefficientY * Mathf.Exp((x * coefficientX)); break;
            case 12:
                y = coefficientY * Mathf.Sqrt(Mathf.Abs((x * coefficientX))); break;
            case 13:
                y = coefficientY * Mathf.Atan((x * coefficientX)); break;
            case 14:
                y = 0; break;
            case 15:
                y = Mathf.Sqrt(Mathf.Abs(4 - x * x)); break;
            case 16:
                y = (2/(2 * Mathf.PI)) * Mathf.Cos(30); break;
            default:
                y = coefficientY * Random.Range(-(x * coefficientX), (x * coefficientX));
                break;
        }
        y = Mathf.Clamp(y, -100, 100);

        return y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            if (1 << collision.gameObject.layer == LayerMask.GetMask("Player") && collision.gameObject != owner)
            {
                PlayerState ps = collision.GetComponent<PlayerState>();
                if (ps.timerInvincibility<= 0)
                {
                    ps.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
            else if (1 << collision.gameObject.layer == LayerMask.GetMask("Ground"))
            {
                Destroy(gameObject);
            }
        }   
        
        
    }
    
}
