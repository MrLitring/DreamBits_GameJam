using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    LineRenderer lineRenderer;

    float timer;
    float cooldown;
    public float step = 0.1f;
    float currentPos;
    public float startPos = -5;
    public float delay = 0;
    int numFormula;
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        cooldown = 0.05f;
        currentPos = startPos;
        numFormula = Random.Range(0, 14);
    }

    // Update is called once per frame
    void Update()
    {
        CreateLine();
        if (timer >= 0) timer -= Time.deltaTime;
    }

    void CreateLine()
    {
        if (delay <= 0) { 
        if (timer <= 0)
        {
            if (currentPos >= -startPos)
            {
                lineRenderer.positionCount = 0;
                numFormula = Random.Range(0, 13);
                currentPos = startPos;
                print(numFormula);
            }            
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.positionCount += 1;

            
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(currentPos, CalculateY(currentPos, numFormula)));
            currentPos += step;
            /*
            Gradient gradient = new Gradient();
            gradient.colorSpace = ColorSpace.Linear;
            gradient.colorKeys = new GradientColorKey[] { new GradientColorKey(Color.yellow, 2f), new GradientColorKey(Color.red, 2f) };
            lineRenderer.colorGradient = gradient;
            */
            timer = cooldown;
        }
        }
        else delay -= Time.deltaTime;
    }

    float CalculateY(float x, int num)
    {
        float y;
        y = x * x;
        switch (num)
        {
            case 0:
                y = x; break;
            case 1:
                y = x * x; break;
            case 2:
                y = Mathf.Pow(x, 1 / 3);
                break;
            case 3:
                y = Mathf.Pow(2, x); break;
            case 4:
                y = Mathf.Pow(0.5f, x);
                break;
            case 5:
                y = -x; break;
            case 6:
                y = - (x * x); break;
            case 7:
                y = 0.5f * Mathf.Pow(2, x); break;
            case 8:
                y = Mathf.Sin(x); break;
            case 9:
                y = Mathf.Cos(x); break;
            case 10:
                y = Mathf.Tan(x); break;
            case 11:
                y = Mathf.Exp(x); break;
            case 12:
                y = Mathf.Sqrt(Mathf.Abs(x)); break;
            case 13:
                y = Mathf.Atan(x); break;
            default:
                y = Random.Range(-x, x);
                break;

        }
        y = Mathf.Clamp(y, -20, 20);

        return y;
    }
}
