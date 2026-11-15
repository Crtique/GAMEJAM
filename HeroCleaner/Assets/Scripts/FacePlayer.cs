using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.forward = mainCameraTransform.forward;
    }
}
