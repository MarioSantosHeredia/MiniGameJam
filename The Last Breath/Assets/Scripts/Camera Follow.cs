using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float fixedY = 0f; // Y fijo para la camara
    public float minX, maxX; // limites horizontales
    public Vector3 offset;

    public bool lockCamera = false;
    public Vector3 lockedPosition;

    void LateUpdate()
    {
        if (lockCamera)
        {
            Vector3 targetPosition = new Vector3(lockedPosition.x, fixedY + offset.y, transform.position.z);
            Vector3 smoothed = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothed;
            return;
        }

        if (target == null) return;

        float clampedX = Mathf.Clamp(target.position.x + offset.x, minX, maxX);

        Vector3 desiredPosition = new Vector3(clampedX, fixedY + offset.y, transform.position.z);
        
        Vector3 smooth = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smooth;
    }
}