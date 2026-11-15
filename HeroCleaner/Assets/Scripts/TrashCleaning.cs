using UnityEngine;

public class TrashCleaning : MonoBehaviour
{
    [Header("Cleaning Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float cleaningDistance = 4f;
    [SerializeField] private float cleaningTime = 2f;

    private float cleaningTimer;
    private Interactable currentTrash;
    private TrashPrompt currentPrompt;

    private void Update()
    {
        CheckForTrash();

        if (currentTrash == null)
        {
            cleaningTimer = 0f;
            return;
        }

        if (Input.GetKey(KeyCode.E))
        {
            cleaningTimer += Time.deltaTime;

            float progress = cleaningTimer / cleaningTime;

            if (currentPrompt != null)
                currentPrompt.UpdateProgress(progress);

            if (cleaningTimer >= cleaningTime)
                CleanTrash();
        }
        else
        {
            cleaningTimer = 0f;

            if (currentPrompt != null)
                currentPrompt.UpdateProgress(0f);
        }
    }

    private void CheckForTrash()
    {
        if (playerCamera == null)
            return;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, cleaningDistance))
        {
            Interactable item = hit.collider.GetComponentInParent<Interactable>();

            if (item != null &&
                item.ItemCategory == Interactable.ItemType.Trash &&
                !item.IsCollected &&
                !item.IsPlaced &&
                !item.IsHeld)
            {
                if (currentTrash != item)
                {
                    HideCurrentPrompt();

                    currentTrash = item;
                    cleaningTimer = 0f;

                    currentPrompt =
                        currentTrash.GetComponentInChildren<TrashPrompt>(true);

                    if (currentPrompt != null)
                        currentPrompt.ShowPrompt(true);
                }

                return;
            }
        }

        HideCurrentPrompt();
        currentTrash = null;
        cleaningTimer = 0f;
    }

    private void CleanTrash()
    {
        if (currentTrash == null)
            return;

        Interactable trashToClean = currentTrash;

        HideCurrentPrompt();

        if (trashToClean.Collect())
        {
            if (QuestManager.instance != null)
                QuestManager.instance.CleanTrash();

            Debug.Log("Trash cleaned!");

            Destroy(trashToClean.gameObject);
        }

        currentTrash = null;
        currentPrompt = null;
        cleaningTimer = 0f;
    }

    private void HideCurrentPrompt()
    {
        if (currentPrompt != null)
            currentPrompt.ShowPrompt(false);

        currentPrompt = null;
    }
}
