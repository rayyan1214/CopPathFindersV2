using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationDuration = 10.0f; 

    private float rotationAngle = 0f;
    public bool rotateFast = false;
    private bool rotatingForward = true;


    void Update()
    {
        RotateObj();
    }

    void RotateObj()
    {
        float rotationStep = (180f / rotationDuration) * Time.deltaTime;
        if (rotateFast) {
            rotationStep += 10;
        }
        if (rotatingForward)
        {
            rotationAngle += rotationStep;
            if (rotationAngle >= 180f)
            {
                rotationAngle = 180f;
                rotatingForward = false;
            }
        }
        else
        {
            rotationAngle -= rotationStep;
            if (rotationAngle <= 0f)
            {
                rotationAngle = 0f;
                rotatingForward = true;
            }
        }

        transform.localRotation = Quaternion.Euler(0, rotationAngle, 0);
    }

    


}
