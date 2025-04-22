using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ProjectileColor : MonoBehaviour
{
    
    public PlayerSettings playerSettings;
    
    void Start()
    {
        Light2D light2D = GetComponent<Light2D>();
        light2D.color = playerSettings.sphereColor;
    }

}
