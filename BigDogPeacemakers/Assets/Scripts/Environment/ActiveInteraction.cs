using UnityEngine;

public class ActiveInteraction : MonoBehaviour
{
    public bool IsActive{ get; private set; }
    void Start()
    {
        IsActive = false;
    }
    
    public void SetBool(bool value)
    {
        IsActive = value;
    }

    void Update()
    {
        SetBool(false);
    }
}
