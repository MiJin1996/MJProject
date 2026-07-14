using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform player;

    public float distance = 8f;
    public float height = 5f;

    void Start()
    {

        bool thirdPersonMode = PlayerPrefs.GetInt("CameraMode", 0) == 1;
        enabled = thirdPersonMode;
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 targetPosition =
            player.position
            - player.forward * distance
            + Vector3.up * height;

        transform.position = targetPosition;

        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}

