using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float lifetime;
    public float speed;
    Rigidbody2D rb;
    public Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        Vector3 mouseWorldPos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector3 objectPos = transform.position;
        Vector3 directionToMouse = mouseWorldPos - objectPos;

        float hipotenusa = Mathf.Sqrt(Mathf.Pow(directionToMouse.x, 2) + Mathf.Pow(directionToMouse.y, 2));
        float katet = directionToMouse.y;
        float degree = (Mathf.Asin(katet / hipotenusa) * 180) / Mathf.PI;
 



        float sum = Mathf.Abs(directionToMouse.x) + Mathf.Abs(directionToMouse.y);
        Vector2 vector = new Vector2(directionToMouse.x / sum, directionToMouse.y / sum);
      
        
        speed = 50;
        rb.AddForce(vector * speed, ForceMode2D.Impulse);
        // = vector * speed;
        lifetime = 3.0f;
        Destroy(gameObject, lifetime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        TestStaticObj staticObj;
        if (collision.CompareTag("Enemy") && collision.TryGetComponent<TestStaticObj>(out staticObj))
        {
            collision.GetComponent<TestStaticObj>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    
}
