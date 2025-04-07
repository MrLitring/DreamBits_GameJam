using UnityEngine;

public class PlayerCheker : MonoBehaviour
{
    public bool isPlayer;
    private void Start()
    {
        isPlayer = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }
}
