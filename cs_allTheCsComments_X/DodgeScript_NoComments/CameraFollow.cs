using UnityEngine;
using UnityEngine.EventSystems;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 7.0f;
    public float height = 3.0f;
    public float smoothTime = 0.2f;

    private Vector3 currentVelocity;

    void Start()
    {

    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredForward = target.forward;
        desiredForward.y = 0f;

        Vector3 targetPosition = target.position - (target.forward * distance) + (Vector3.up * height);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        transform.LookAt(target.position + Vector3.up * 1.5f);

    }

}
