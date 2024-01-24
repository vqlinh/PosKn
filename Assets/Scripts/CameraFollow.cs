using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float offet = 2f;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = new Vector3(player.position.x + offet, transform.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
