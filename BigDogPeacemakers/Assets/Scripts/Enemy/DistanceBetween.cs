using UnityEngine;

public class DistanceBetween : MonoBehaviour
{
    public Transform other;
    Transform thisObject;
    public float distance { get; private set; }
    void Start()
    {
        thisObject = transform;
    }

    
    void Update()
    {
        if (other != null) distance = Vector2.Distance(other.position, thisObject.position);
    }
}
