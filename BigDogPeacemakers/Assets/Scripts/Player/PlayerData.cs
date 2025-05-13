using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerData : MonoBehaviour
{
    private Light2D lightOfSphere;
    
    public Color SphereColor {  get; private set; }
    void Awake()
    {
        lightOfSphere = transform.Find("EnergySphere").GetComponent<Light2D>();
        //lightOfSphere.color = settings.sphereColor;
        SphereColor = lightOfSphere.color;
    }

    public void ChangeColor(Color color)
    {
        lightOfSphere.color = color;
        SphereColor = color;
    }
}
