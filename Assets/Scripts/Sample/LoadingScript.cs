using UnityEngine;

public class LoadingScript : MonoBehaviour
{
    public RectTransform image;
    public float timeStep;
    public float oneStepAngle;

    float startTime;

    void Start()
    {
        startTime = Time.time;    
    }

    void Update()
    {
        if (Time.time - startTime >= timeStep)
        {
            Vector3 imageAngle = image.localEulerAngles;
            imageAngle.z += oneStepAngle;
            image.localEulerAngles = imageAngle;
            startTime = Time.time;
        }
    }
}
