using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Rigidbody objectRb;
    private Transform objectGrabTransform;

    private void Start()
    {
        objectRb = GetComponent<Rigidbody>();
    }

    public void DropObject()
    {
        this.objectGrabTransform = null;
        objectRb.useGravity = true;
    }

    public void GrabObjects(Transform objectGrabTransform)
    {
        this.objectGrabTransform = objectGrabTransform;
        objectRb.useGravity = false;
    }

    private void FixedUpdate()
    {
        if (objectGrabTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPos = Vector3.Lerp(transform.position, objectGrabTransform.position, Time.deltaTime * lerpSpeed);
            objectRb.MovePosition(newPos);
        }
    }
}
