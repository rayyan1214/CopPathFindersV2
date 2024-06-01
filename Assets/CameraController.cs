using UnityEngine;


public class CameraController : MonoBehaviour
{
    public float rotationDuration = 10.0f;
    public Light spotlight;
    public LayerMask detectableLayers; 
    public bool inverseRotation = false; 

    private float rotationAngle = 0f;
    private bool rotatingForward = true;

    void Update()
    {
        RotateCamera();
        DetectObjects();
    }

    void RotateCamera()
    {
        float directionFactor = inverseRotation ? -1f : 1f;

        float rotationStep = (180f / rotationDuration) * Time.deltaTime * directionFactor;

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
    
    
    void DetectObjects()
    {
        Vector3 origin = spotlight.transform.position;
        Vector3 direction = spotlight.transform.forward;
        float maxDistance = spotlight.range;

        float endRadius = maxDistance * Mathf.Tan(spotlight.spotAngle * Mathf.Deg2Rad / 2);

        RaycastHit[] hits = Physics.SphereCastAll(origin, endRadius, direction, maxDistance, detectableLayers);

        foreach (RaycastHit hit in hits)
        {
            Vector3 directionToObject = hit.point - origin;
            float angleToObject = Vector3.Angle(direction, directionToObject);

            if (angleToObject < spotlight.spotAngle / 2)
            {
                PerformAction(hit.collider.gameObject);
            }
        }
    }

    void PerformAction(GameObject obj)
    {
        obj.transform.position = new Vector3(2f, 0f, 32f);
        Debug.Log($"Object {obj.name} sdpotted");
    }
}
