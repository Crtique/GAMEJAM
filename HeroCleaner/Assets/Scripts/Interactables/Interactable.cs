using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    public enum ItemType
    {
        ShelfOneItem,
        ShelfTwoItem,
        Clothing,
        Trash
    }

    [Header("Item Settings")]
    [SerializeField] private ItemType itemType;

    private Rigidbody objectRb;
    private Transform objectGrabTransform;

    public ItemType ItemCategory => itemType;
    public bool IsHeld => objectGrabTransform != null;
    public bool IsPlaced { get; private set; }
    public bool IsCollected { get; private set; }

    private void Start()
    {
        objectRb = GetComponent<Rigidbody>();
    }

    public void DropObject()
    {
        this.objectGrabTransform = null;

        if (IsPlaced || IsCollected) return;

        objectRb.useGravity = true;
    }

    public void GrabObjects(Transform objectGrabTransform)
    {
        if (IsPlaced || IsCollected) return;
        this.objectGrabTransform = objectGrabTransform;
        objectRb.useGravity = false;
    }


    public bool PlaceAt(Transform placementPoint)
    {
        if (IsHeld || IsPlaced || IsCollected)
            return false;

        objectRb.linearVelocity = Vector3.zero;
        objectRb.angularVelocity = Vector3.zero;

        objectRb.useGravity = false;
        objectRb.isKinematic = true;

        transform.SetPositionAndRotation(
            placementPoint.position,
            placementPoint.rotation
        );

        transform.SetParent(placementPoint);

        IsPlaced = true;
        return true;
    }

    public bool Collect()
    {
        if (IsHeld || IsPlaced || IsCollected)
            return false;

        IsCollected = true;
        return true;
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
