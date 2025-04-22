using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerData : MonoBehaviour
{
    private Light2D lightOfSphere;
    public PlayerSettings settings;
    public Color SphereColor {  get; private set; }
    void Awake()
    {
        lightOfSphere = transform.Find("EnergySphere").GetComponent<Light2D>();
        lightOfSphere.color = settings.sphereColor;
        SphereColor = lightOfSphere.color;
    }

    public void ChangeColor(Color color)
    {
        settings.sphereColor = color;
        lightOfSphere.color = settings.sphereColor;
        SphereColor = lightOfSphere.color;
    }
}
