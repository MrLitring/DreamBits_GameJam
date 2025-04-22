using UnityEngine;

public class MoveBetween : MonoBehaviour
{
    public float speed = 10;
    public float delay = 0;
    public float cooldown = 0;
    float timer = 0;
    bool AtoB;

    Transform A;
    Transform B;
    Rigidbody2D rb;
    void Start()
    {
        A = transform.parent.Find("A");
        B = transform.parent.Find("B");
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (delay <= 0)
        {
            if(timer <= 0)
            {
                if (AtoB)
                {
                    if (rb.position != (Vector2)B.position)
                    {
                        Vector2 vector = Vector2.Lerp(rb.position, B.position, Time.deltaTime * speed);
                        rb.MovePosition(vector);
                    }
                    else
                    {
                        AtoB = !AtoB;
                    }
                }
                else
                {
                    if (rb.position != (Vector2)A.position)
                    {
                        Vector2 vector = Vector2.Lerp(rb.position, A.position, Time.deltaTime * speed);
                        rb.MovePosition(vector);
                    }
                    else
                    {
                        AtoB = !AtoB;
                    }
                }
                timer = cooldown;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else
        {
            delay -= Time.deltaTime;
        }     
    }
}
