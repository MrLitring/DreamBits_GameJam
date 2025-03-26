using UnityEngine;

public class TemplateCameraController : MonoBehaviour
{
    public Transform player;
    public Vector2 offset;
    public float z_pos;

    private void Start()
    {
        offset = new Vector2(7, 3);
        z_pos = -10;
    }


    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, z_pos);
    }
}
