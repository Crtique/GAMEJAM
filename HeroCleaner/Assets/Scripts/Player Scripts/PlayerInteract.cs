using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interaction References")]
    [SerializeField] private Transform camTransform;
    [SerializeField] private Transform objectGrabTransform;
    [SerializeField] private LayerMask pickUpMask;

    [Header("Placement Settings")]
    [SerializeField] private float dropDistance = 5f;
    [SerializeField] private LayerMask shelfMask;

    private Interactable interact;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (interact == null)
            {
                // When not carrying an object try to grab it
                float pickUpDistance = 5f;

                if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, pickUpDistance, pickUpMask))
                {
                    if (hit.collider.TryGetComponent<Interactable>(out interact))
                    {
                        // Prevent picking up trash.
                        if (interact.ItemCategory == Interactable.ItemType.Trash)
                        {
                            interact = null;
                            return;
                        }

                        interact.GrabObjects(objectGrabTransform);
                    }

                }
            }
            else
            {
                // Try to place the object on a shelf
                if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, dropDistance, shelfMask))
                {
                    ShelfPlacements shelf = hit.collider.GetComponentInParent<ShelfPlacements>();

                    if (shelf != null)
                    {
                        bool placed = shelf.TryPlaceObject(interact);

                        if (placed)
                        {
                            interact = null;
                            return;
                        }
                    }
                }
                // Currently we are carrying something, drop object
                interact.DropObject();
                interact = null; 
            }
        }
    }
}
