using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]

public class QuestData : ScriptableObject
{
    public string questName;

    [Header("Objectives")]
    public int trashRequiered;
    public int shelfTypeOneRequired;
    public int shelfTypeTwoRequired;
    public int laundryRequired;

    [Header("Reward")]
    public int reward;
}
