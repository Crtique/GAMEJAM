using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform camTransform;
    [SerializeField] private Transform objectGrabTransform;
    [SerializeField] private LayerMask pickUpMask;

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
                    if (hit.transform.TryGetComponent<Interactable>(out interact))
                    {
                        interact.GrabObjects(objectGrabTransform);
                    }
                }
            }
            else
            {
                // Currently we are carrying something, drop object
                interact.DropObject();
                interact = null; 
            }
        }
    }
}
