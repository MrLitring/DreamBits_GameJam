using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    public bool WallIsHere { get; private set; }
    Collider2D colliderThis;
    ContactFilter2D filter;
    List<Collider2D> collidersThis;
    void Start()
    {
        colliderThis = GetComponent<Collider2D>();
        filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Ground"));
        collidersThis = new List<Collider2D>(); 
    }

    void Update()
    {
        colliderThis.Overlap(filter, collidersThis);
        if (collidersThis.Count > 0 ) WallIsHere = true;
        else WallIsHere = false;

    }
}
