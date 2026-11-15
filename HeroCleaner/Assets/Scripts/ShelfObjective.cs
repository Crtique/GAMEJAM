using System.Collections.Generic;
using UnityEngine;

public class ShelfObjective : MonoBehaviour
{
    public enum ShelfType
    {
        shelfOne,
        shelfTwo
    }

    [SerializeField] private ShelfType shelfType;

    private HashSet<GameObject> shelfObjects = new HashSet<GameObject>();
    

    public void RegisterItems(GameObject item)
    {
        if (!shelfObjects.Add(item)) return;

        if (shelfType == ShelfType.shelfOne)
        {
            QuestManager.instance.OrganizeShelfTypeOne();
        }
        else if (shelfType == ShelfType.shelfTwo)
        {
            QuestManager.instance.OrganizeShelfTypeTwo();
        }
    }
}
