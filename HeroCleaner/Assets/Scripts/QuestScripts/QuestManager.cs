using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [Header("Quest Refrences")]
    [SerializeField] private QuestData currQuest;
    [SerializeField] private QuestUI questUI;
    [SerializeField] private QuestData startingQuest;

    [Header("Quest Progress")]
    private int trashCollected;
    private int clothesInLaundry;
    private int shelfOneOrganized;
    private int shelfTwoOrganized;

    // Grab the Win Screen
    [SerializeField] private Canvas winScreen;

    bool questCompleted;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartQuest(startingQuest);
        winScreen.enabled = false;
    }

    // ----- START GAME ----- //
    public void StartQuest(QuestData quest)
    {
        currQuest = quest;

        // Reset Progress
        trashCollected = 0;
        clothesInLaundry = 0;
        shelfOneOrganized = 0;
        shelfTwoOrganized = 0;

        questCompleted = false;

        UpdateUI();
    }

    // ----- TRASH CLEANING ----- //
    public void CleanTrash()
    {
        if (currQuest == null || questCompleted) return;

        trashCollected++;

        UpdateUI();
        CheckQuestProgress();
    }

    // ----- AWARD SHELF ----- //
    public void OrganizeShelfTypeOne()
    {
        if (currQuest == null || questCompleted)
            return;

        if (shelfOneOrganized >= currQuest.shelfTypeOneRequired)
            return;

        shelfOneOrganized++;

        UpdateUI();
        CheckQuestProgress();
    }

    // ----- VILLIAN SHELF ----- //
    public void OrganizeShelfTypeTwo()
    {
        if (currQuest == null || questCompleted)
            return;

        if (shelfTwoOrganized >= currQuest.shelfTypeTwoRequired)
            return;

        shelfTwoOrganized++;

        UpdateUI();
        CheckQuestProgress();
    }

    // ----- LAUNDRY ----- //
    public void PutClothesInLaundry()
    {
        if (currQuest == null || questCompleted)
            return;

        if (clothesInLaundry >= currQuest.laundryRequired)
            return;

        clothesInLaundry++;

        UpdateUI();
        CheckQuestProgress();
    }

    // ----- UPDATE THE UI ------ //
    void UpdateUI()
    {
        if (questUI == null || currQuest == null) return;

        questUI.UpdateQuestUI(currQuest, trashCollected, clothesInLaundry, shelfOneOrganized, shelfTwoOrganized);
    }

    // ----- CHECK THE PROGRESS OF TASK ----- //
    void CheckQuestProgress()
    {
        if (trashCollected >= currQuest.trashRequiered && clothesInLaundry >= currQuest.laundryRequired && shelfOneOrganized >= currQuest.shelfTypeOneRequired && shelfTwoOrganized >= currQuest.shelfTypeTwoRequired)
        {
            CompleteQuest();
            Debug.Log("Quest Finshed");

            // Win Screen Logic
            if (questCompleted)
            {
                Time.timeScale = 0;
                winScreen.enabled = true;
            }
        }
    }

    void CompleteQuest()
    {
        questCompleted = true;
    }
}
