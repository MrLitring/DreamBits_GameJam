using UnityEngine;
using System.Collections.Generic;

public class PlayerInteract : MonoBehaviour
{
    public LayerMask interactiveLayer;

    private bool isInteract;
    Collider2D colliderThis;
    List<Collider2D> collidersOverlap;
    void Start()
    {
        colliderThis = GetComponent<Collider2D>();
        collidersOverlap = new List<Collider2D>();
        isInteract = false;
    }

    public void UpdateInputData(bool isInteract)
    {
        this.isInteract = isInteract;
    }

    
    void Update()
    {
        if (isInteract) {
            colliderThis.Overlap(collidersOverlap);
            foreach (var colliderI in collidersOverlap)
            {
                if (1 << colliderI.gameObject.layer == interactiveLayer)
                {
                    ActiveInteraction interObj;
                    if(colliderI.TryGetComponent<ActiveInteraction>(out interObj))
                    {
                        interObj.SetBool(true);
                    }
                }
            }
        }

        


    }
}
