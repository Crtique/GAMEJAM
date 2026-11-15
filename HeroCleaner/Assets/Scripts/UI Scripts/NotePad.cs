using UnityEngine;

public class NotePad : MonoBehaviour
{
    [SerializeField] private Canvas notePadScreen;
    [SerializeField] private StartingNotePadCloser startingNotepad;
    [SerializeField] private AudioSource notpadSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        notePadScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startingNotepad.closed) return;


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Play Notepad Sound
            notpadSound.enabled = true;

            Time.timeScale = 0.0f;
            notePadScreen.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            notpadSound.enabled = false;

            Time.timeScale = 1.0f;
            notePadScreen.enabled = false;
        }
    }
}
