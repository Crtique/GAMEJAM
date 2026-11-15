using UnityEngine;

public class ShelfPlacements : MonoBehaviour
{
    public enum ShelfType
    {
        AwardShelf,
        VillianShelf
    }

    [Header("Shelf Settings")]
    [SerializeField] private ShelfType shelfType;

    [SerializeField] private Interactable.ItemType acceptItemType;
    [SerializeField] private Transform[] placementPoint;

    bool[] placePoints;

    private void Awake()
    {
        placePoints = new bool[placementPoint.Length];
    }
    private void OnTriggerStay(Collider other)
    {
        Interactable item = other.GetComponentInParent<Interactable>();

        if (item == null || item.IsHeld || item.IsPlaced || item.IsCollected) return;

        if (item.ItemCategory != acceptItemType) return;

        TryPlaceObject(item);
    }
    public bool TryPlaceObject (Interactable item)
    {
        if (item == null || item.IsHeld || item.IsPlaced || item.IsCollected)
            return false;

        for (int i = 0; i < placementPoint.Length; i++)
        {
            if (placePoints[i])
                continue;

            if (item.PlaceAt(placementPoint[i]))
            {
                placePoints[i] = true;

                if (QuestManager.instance != null)
                {
                    if (shelfType == ShelfType.AwardShelf)
                    {
                        QuestManager.instance.OrganizeShelfTypeOne();
                    }
                    else if (shelfType == ShelfType.VillianShelf)
                    {
                        QuestManager.instance.OrganizeShelfTypeTwo();
                    }
                }

                Debug.Log(item.name + " placed successfully!");

                return true;
            }
        }

        Debug.Log("This shelf is full!");
        return false;
    }
}
