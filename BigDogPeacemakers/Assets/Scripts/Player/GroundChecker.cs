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

        var results = new List<Collider2D>();  
        thisCollider.Overlap(results);  
        IsGrounded = false;
        foreach (var col in results)
        {
            if (1 << col.gameObject.layer == groundLayer)
            {
                IsGrounded = true; break;
            } 
        }
    }
}
