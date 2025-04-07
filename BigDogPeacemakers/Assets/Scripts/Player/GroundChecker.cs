using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField]private LayerMask groundLayer;
    private LayerMask thisLayer;
    private ContactFilter2D filter;
    private Collider2D thisCollider;
    private List<Collider2D> currentColliders;
    private List<Collider2D> oldColliders;
    public bool IsGrounded {  get; private set; }

    private void Start()
    {
        thisLayer = 1 << gameObject.layer;
        filter = new ContactFilter2D();
        filter.SetLayerMask(thisLayer);
        Collider2D[] colls = GetComponents<Collider2D>();
        
        foreach (var col in colls)
        {
            if (col.isTrigger)
            {
                thisCollider = col;
                break;
            }
        }
        
    }

    private void Update()
    {

        var results = new List<Collider2D>();  // Список для хранения результатов
        thisCollider.Overlap(results);  // Заполняем список коллайдерами, пересекающими parentCollider
        IsGrounded = false;
        foreach (var col in results)
        {
            if (1 << col.gameObject.layer == groundLayer)
            {
                IsGrounded = true; break;
            } 
            Debug.Log("Обнаружен объект: " + col.name);  // Вывод имён пересекающихся объектов
        }
        /*
        if (results.Count > 0) IsGrounded = true;
        else IsGrounded = false;

        
        currentColliders = new List<Collider2D>();
        thisCollider.Overlap(filter, currentColliders);
        if (oldColliders == null) oldColliders = currentColliders;
        if (currentColliders != oldColliders)
        {
            IsGrounded = false;
            foreach (var col in oldColliders)
            {
                if (1 << col.gameObject.layer == groundLayer)
                {
                    IsGrounded = true;
                    break;
                }
            }
            oldColliders = currentColliders;
            foreach (var col in currentColliders) print(col.name);
        }
        if (currentColliders.Count == 0) print("Коллайдеры пусты");
        */
    }


    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        LayerMask layerMask = 1 << collision.gameObject.layer;

        if (layerMask == groundLayer) IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        LayerMask layerMask = 1 << collision.gameObject.layer;

        if (layerMask == groundLayer) IsGrounded = false;
    }
    */
}
