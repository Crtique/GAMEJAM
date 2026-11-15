using UnityEngine;

public class LaundryPlacements : MonoBehaviour
{
    [Header("Laundry Settings")]
    [SerializeField] private Transform[] placementPoints;

    private bool[] placePoints;

    private void Awake()
    {
        placePoints = new bool[placementPoints.Length];
    }

    private void OnTriggerStay(Collider other)
    {
        Interactable item = other.GetComponentInParent<Interactable>();

        // Make sure the object is valid and has been dropped
        if (item == null || item.IsHeld || item.IsPlaced || item.IsCollected)
            return;

        // Only allow clothing in the laundry basket
        if (item.ItemCategory != Interactable.ItemType.Clothing)
            return;

        // Find an available placement point
        for (int i = 0; i < placementPoints.Length; i++)
        {
            if (placePoints[i])
                continue;

            if (item.PlaceAt(placementPoints[i]))
            {
                placePoints[i] = true;

                if (QuestManager.instance != null)
                {
                    QuestManager.instance.PutClothesInLaundry();
                }

                Debug.Log("Clothing placed in laundry basket!");
                return;
            }
        }

        Debug.Log("The laundry basket is full!");
    }
}
