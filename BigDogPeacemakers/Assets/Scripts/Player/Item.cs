using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;
    protected SpriteRenderer spriteRenderer;
    public Sprite sprite;

    private void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
        spriteRenderer.sprite = sprite;
    }

}
