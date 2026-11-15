using UnityEngine;
using TMPro;


public class QuestUI : MonoBehaviour
{
    [Header("Notepad Refrences")]
    [SerializeField] private TMP_Text taskText; 
    [SerializeField] private TMP_Text trashText; 
    [SerializeField] private TMP_Text clothesText; 
    [SerializeField] private TMP_Text shelfOneText; 
    [SerializeField] private TMP_Text shelfTwoText; 

    public void UpdateQuestUI(QuestData quest, int trash, int laundry, int shelfOne, int ShelfTwo)
    {
        taskText.text = "Task: " + quest.questName;

        trashText.text = "Trash: " + trash + "/" + quest.trashRequiered;
        clothesText.text = "Gather Clothes: " + laundry + "/" + quest.laundryRequired;
        shelfOneText.text = "Organize Shelf 1: " + shelfOne + "/" + quest.shelfTypeOneRequired;
        shelfTwoText.text = "Organize Shelf 2: " + ShelfTwo + "/" + quest.shelfTypeTwoRequired;
    }
}
