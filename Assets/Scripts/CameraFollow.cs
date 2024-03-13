using UnityEngine;

public class CameraFollow : MonoBehaviour
{
     private Transform player;
     private float smoothSpeed = 0.125f;
     private float offet = 2f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Const.player).transform;
    }

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
