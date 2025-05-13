using UnityEngine;

public class KillerZone : MonoBehaviour
{
    GameControllerLocal gameControllerLocal;
    private void Start()
    {
        gameControllerLocal = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerLocal>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == LayerMask.GetMask("Player"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
