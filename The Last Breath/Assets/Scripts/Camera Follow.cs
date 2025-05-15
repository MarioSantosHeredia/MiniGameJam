using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float fixedY = 0f; // Y fijo para la camara
    public float minX, maxX; // limites horizontales
    public Vector3 offset;

    void LateUpdate()
    {
        if (target == null) return;

        float clampedX = Mathf.Clamp(target.position.x + offset.x, minX, maxX);

        Vector3 desiredPosition = new Vector3(clampedX, fixedY + offset.y, transform.position.z);

        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothed;
    }
}